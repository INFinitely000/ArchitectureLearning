using System;
using Service;
using UnityEngine;
using Gameplay.MainPlayer;
using TMPro;
using UnityEngine.UI;

namespace Gameplay.UI
{
    public class HealthUI : MonoBehaviour
    {
        [SerializeField] private Image _fillImage;
        [SerializeField] private TextMeshProUGUI _text;
        
        public IHealthable Healthable { get; private set; }

        public void SetHealth(IHealthable healthable)
        {
            if (Healthable != null) Healthable.Changed -= OnChanged;
            
            Healthable = healthable;
            
            if (Healthable != null) Healthable.Changed += OnChanged;
            
            OnChanged(0);
        }

        private void OnChanged(int difference)
        {
            _fillImage.fillAmount = (float)Healthable.Health / Healthable.MaxHealth;

            _text.text = string.Format("{0}/{1}", Healthable.Health.ToString(), Healthable.MaxHealth.ToString());
        }
    }
}