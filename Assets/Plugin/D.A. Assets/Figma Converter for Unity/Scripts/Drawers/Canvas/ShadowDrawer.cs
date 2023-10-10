using DA_Assets.FCU.Model;
using System;
using DA_Assets.Shared;
using DA_Assets.Shared.Extensions;
using DA_Assets.FCU.Extensions;

#if TRUESHADOW_EXISTS
using LeTai.TrueShadow;
#endif

namespace DA_Assets.FCU.Drawers.CanvasDrawers
{
    [Serializable]
    public class ShadowDrawer : MonoBehaviourBinder<FigmaConverterUnity>
    {
        public void Draw(FObject fobject)
        {
            switch (monoBeh.Settings.ComponentSettings.ShadowComponent)
            {
                case ShadowComponent.TrueShadow:
                    DrawTrueShadow(fobject);
                    break;
            }
        }

        private void DrawTrueShadow(FObject fobject)
        {
#if TRUESHADOW_EXISTS
            TrueShadow[] oldShadows = fobject.Data.GameObject.GetComponents<TrueShadow>();

            foreach (TrueShadow oldShadow in oldShadows)
                oldShadow.Destroy();

            foreach (Effect effect in fobject.Effects)
            {
                if (effect.Type.Contains("SHADOW"))
                {
                    monoBeh.Log($"DrawTrueShadow | {fobject.Data.Hierarchy}", FcuLogType.SetTag);

                    fobject.Data.GameObject.TryAddComponent(out TrueShadow trueShadow);

                    trueShadow.Offset.Set(effect.Offset.x, effect.Offset.y);
                    trueShadow.Color = effect.Color;
                    trueShadow.Size = effect.Radius;
                    trueShadow.BlendMode = BlendMode.Multiply;

                    if (effect.Type.Contains("DROP"))
                        trueShadow.Inset = false;
                    else
                        trueShadow.Inset = true;

                    trueShadow.enabled = effect.Visible.ToBoolNullFalse();
                }
            }
#endif
        }
    }
}