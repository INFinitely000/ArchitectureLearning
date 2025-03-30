using System;
using Service;
using TMPro;
using UnityEngine;

namespace Gameplay.UI
{
    public class WalletUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _coinsBar;


        private void OnEnable()
        {
            Services.Instance.Single<IWallet>().Changed += Refresh;
            
            Refresh(0);
        }

        private void OnDisable()
        {
            Services.Instance.Single<IWallet>().Changed -= Refresh;
        }

        private void Refresh(int difference)
        {
            _coinsBar.text = Services.Instance.Single<IWallet>().Coins.ToString();
        }
    }
}