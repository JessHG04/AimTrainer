using UnityEngine;
using UnityEngine.SceneManagement;

public class TargetHitted : MonoBehaviour {
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite _targetSprite;
    [SerializeField] private Sprite _birdSprite;
    private void Start() {
        if(SceneManager.GetActiveScene().name == "Motionless Target Scene") {
            _spriteRenderer.sprite = _targetSprite;
        } else {
            _spriteRenderer.sprite = _birdSprite;
        }
        Destroy(gameObject, 0.5f);
    }
}
