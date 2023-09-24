using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    [SerializeField] private Image _image;
    
    public void SetBackground(Sprite sprite) => _image.sprite = sprite;
}
