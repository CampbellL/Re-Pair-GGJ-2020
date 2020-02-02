using UnityEngine;

namespace Source.Animals
{
    public class SlowAnimal : Animal
    {
        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            SetSpritesForSeason();
        }
    
        protected void SetSpritesForSeason()
        {
            sprRndr.sprite = normalSprites[GameMode.instance.season];
        }
    }
}
