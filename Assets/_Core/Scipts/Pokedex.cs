using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Newtonsoft.Json.Linq;
using TMPro;

public class Pokedex : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _orderText;
    
    private int _index = 0;
    private int _maxIndex;
    private const string _apiUrl = "https://pokeapi.co/api/v2/pokemon";
    
    private void Awake()
    {
        StartCoroutine(GetMaxIndex());
    }

    private void Start()
    {
        StartCoroutine(GetPokemon());
    }
    
    string RequestError(UnityWebRequest request)
    {
        return $"Erreur de requette : {request.error}";
    }

    private IEnumerator GetPokemon()
    {
        using (UnityWebRequest pokemonRequest = UnityWebRequest.Get($"{_apiUrl}/?offset=0&limit={_maxIndex}"))
        {
            yield return pokemonRequest.SendWebRequest();
            if (pokemonRequest.result != UnityWebRequest.Result.Success)
            {
                RequestError(pokemonRequest);
            }
            else
            {
                JObject jsonPokemon = JObject.Parse(pokemonRequest.downloadHandler.text);
                string urlWithIndex = jsonPokemon["results"][_index]["url"].ToString();
                
                using (UnityWebRequest pokemonDataRequest = UnityWebRequest.Get(urlWithIndex))
                {
                    yield return pokemonDataRequest.SendWebRequest();
                    if (pokemonDataRequest.result != UnityWebRequest.Result.Success)
                    {
                        RequestError(pokemonDataRequest);
                    }
                    else
                    {
                        JObject jsonPokemonData = JObject.Parse(pokemonDataRequest.downloadHandler.text);

                        Sprite sprites = null;
                
                        using (UnityWebRequest textureRequest = UnityWebRequestTexture.GetTexture(jsonPokemonData["sprites"]["front_default"].ToString()))
                        {
                            yield return textureRequest.SendWebRequest();
                            if (textureRequest.result != UnityWebRequest.Result.Success)
                            {
                                RequestError(textureRequest);
                            }
                            else
                            {
                                Sprite ConvertTextureToSprite(Texture2D texture)
                                {
                                    return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height),
                                        new Vector2(.5f, .5f));
                                }
                        
                                Texture2D texture = ((DownloadHandlerTexture)textureRequest.downloadHandler).texture;
                                sprites = ConvertTextureToSprite(texture);
                            }
                        }
                
                        Pokemon pokemon = new()
                        {
                            Name = jsonPokemonData["name"].ToString(),
                            Id = int.Parse(jsonPokemonData["id"].ToString()),
                            Sprites = sprites,
                        };
                
                        SetPokemonDataUI(pokemon);
                    }
                }
            }
        }
    }
    
    private IEnumerator GetMaxIndex()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(_apiUrl))
        {
            yield return request.SendWebRequest();
            if (request.result != UnityWebRequest.Result.Success)
            {
                RequestError(request);
            }
            else
            {
                JObject json = JObject.Parse(request.downloadHandler.text);
                _maxIndex = int.Parse(json["count"].ToString());
            }
        }
    }
    
    private void SetPokemonDataUI(Pokemon pokemon)
    {
        _nameText.text = pokemon.Name;
        _orderText.text = string.Format($"{pokemon.Id:D3}");
        _image.sprite = pokemon.Sprites;
    }

    public void NextPokemon()
    {
        if (_index + 1 < _maxIndex)
        {
            _index++;
        }
        else
        {
            _index = 0;
        }

        StartCoroutine(GetPokemon());
    }

    public void PreviousPokemon()
    {
        if (_index - 1 >= 0)
        {
            _index--;
        }
        else
        {
            _index = _maxIndex - 1;
        }

        StartCoroutine(GetPokemon());
    }
}

[System.Serializable]
public class Pokemon
{
    public string Name;
    public int Id;
    public Sprite Sprites;
}
