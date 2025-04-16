using System;
using System.Collections;
using UnityEngine;

public class AnimatedTile : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private float _speed;

    private int _currentIndex;


    private void OnEnable() => 
        StartCoroutine(PlayAnimation());


    private IEnumerator PlayAnimation()
    {
        while (isActiveAndEnabled)
        {
            _renderer.sprite = GetCurrentSprite();
            
            NextIndex();
            
            yield return new WaitForSeconds(1f / _speed);
        }
    }

    private Sprite GetCurrentSprite() =>
        _sprites[_currentIndex];

    private void NextIndex() => 
        _currentIndex = _currentIndex + 1 >= _sprites.Length ? 0 : (_currentIndex + 1);
}