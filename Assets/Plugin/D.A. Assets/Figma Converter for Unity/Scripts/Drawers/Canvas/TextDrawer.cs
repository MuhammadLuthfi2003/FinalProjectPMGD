using DA_Assets.FCU.Model;
using DA_Assets.Shared;
using DA_Assets.Shared.Extensions;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#pragma warning disable CS0649

namespace DA_Assets.FCU.Drawers.CanvasDrawers
{
    [Serializable]
    public class TextDrawer : MonoBehaviourBinder<FigmaConverterUnity>
    {
        public void Draw(FObject fobject)
        {
            if (fobject.Data.GameObject.IsPartOfAnyPrefab() == false)
            {
                if (fobject.Data.GameObject.TryGetComponent(out Graphic oldGraphic))
                {
                    oldGraphic.Destroy();
                }
            }

            switch (monoBeh.Settings.ComponentSettings.TextComponent)
            {
#if TextMeshPro
                case TextComponent.TextMeshPro:
                    this.TextMeshDrawer.Draw(fobject);
                    break;
#endif
                default:
                    this.UnityTextDrawer.Draw(fobject);
                    break;
            }
        }

        [SerializeField] UnityTextDrawer unityTextDrawer;
        [SerializeProperty(nameof(unityTextDrawer))]
        public UnityTextDrawer UnityTextDrawer => unityTextDrawer.SetMonoBehaviour(monoBeh);

        [SerializeField] TextMeshDrawer textMeshDrawer;
        [SerializeProperty(nameof(textMeshDrawer))]
        public TextMeshDrawer TextMeshDrawer => textMeshDrawer.SetMonoBehaviour(monoBeh);

        [SerializeField] ModularTextDrawer modularTextDrawer;
        [SerializeProperty(nameof(modularTextDrawer))]
        public ModularTextDrawer ModularTextDrawer => modularTextDrawer.SetMonoBehaviour(monoBeh);
    }
}
