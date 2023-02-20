using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HangmanController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _wordToGuessText;
    [SerializeField] private int hp = 7;
    [SerializeField] private TextMeshProUGUI _textWinLose;
        
    private readonly List<char> _guessedLetters = new();
    private readonly List<char> _wrongTriedLetter = new();
    private readonly int a = 1;

    private readonly string[] _words =
    {
        "Yellow",
        "Black",
        "Red",
        "Green",
        "Grey"
    };

    private string _wordToGuess = "";
    private KeyCode _lastKeyPressed;

    private void Start()
    {
        var randomIndex = Random.Range(0, _words.Length);

        _wordToGuess = _words[randomIndex];
        // Readonly things:
        // a = 2;
        // a = a + 2;
        // _wrongTriedLetter.Add('c');
        // _wrongTriedLetter = new List<char>();
    }


    private void OnGUI()
    {
        var e = Event.current;
        // ВНИЗУ ТО ЖЕ САМОЕ
        // if (e.isKey)
        // {
        //     if (e.keyCode != KeyCode.None && _lastKeyPressed != e.keyCode)
        //     {
        //         ProcessKey(e.keyCode);
        //             
        //         _lastKeyPressed = e.keyCode;
        //     }
        // }
        
        
        // убираем вложенность
        if (!e.isKey) return;
        if (e.keyCode == KeyCode.None || _lastKeyPressed == e.keyCode) return;
        
        ProcessKey(e.keyCode);
                    
        _lastKeyPressed = e.keyCode;
    }

    private void ProcessKey(KeyCode key)
    {
        print("Key Pressed: " + key);

        char pressedKeyString = key.ToString()[0];

        string wordUppercase = _wordToGuess.ToUpper();
            
        bool wordContainsPressedKey = wordUppercase.Contains(pressedKeyString);
        bool letterWasGuessed = _guessedLetters.Contains(pressedKeyString);

        if (!wordContainsPressedKey && !_wrongTriedLetter.Contains(pressedKeyString))
        {
            _wrongTriedLetter.Add(pressedKeyString);
            hp -= 1;

            if (hp <= 0)
            {
                print("You Lost!");
            }
            else
            {
                print("Wrong letter! Hp left = " + hp);
            }
        }
            
        if (wordContainsPressedKey && !letterWasGuessed)
        {
            _guessedLetters.Add(pressedKeyString);
        }

        var stringToPrint = "";
        
        foreach (char letterInWord in wordUppercase)
        {
            if (_guessedLetters.Contains(letterInWord))
            {
                stringToPrint += letterInWord;
            }
            else
            {
                stringToPrint += "_";
            }
        }

        if (wordUppercase == stringToPrint)
        {
            var win = "You win";
            print("You win!");
            _textWinLose.text = win;
        }
            
        print(stringToPrint);
        _wordToGuessText.text = stringToPrint;
    }
}