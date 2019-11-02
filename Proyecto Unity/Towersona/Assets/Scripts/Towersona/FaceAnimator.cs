using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Permite cambiar la cara de una towersona desde animaciones. 
/// En un AnimationClip, hay que ir cambiando la propiedad "currentFace". 
/// Las caras tienen que estar importadas como Sprites, aunque después las tratemos como texturas. 
/// Si no no se pueden poner facilmente en la ventana de animación.
/// </summary>
[ExecuteAlways, RequireComponent(typeof(Animator))]
public class FaceAnimator : MonoBehaviour
{
    [SerializeField]
    private int faceMaterialIndex = 1;
    [SerializeField]
    private string faceTexturePropertyName = "_MainTex";

    [SerializeField, HideInInspector]   //We serialize it so that the Animator can access it. We don't want it to be available in the inspector.
    private Sprite currentFace;

    private Material faceMaterial;

    //Esta mierda no está documentada en absoluto, pero es un evento al estilo de Update para cuando el Animator aplica keyframes.
    //https://forum.unity.com/threads/help-please-with-animation-component-public-properties-custom-inspector.229328/?_ga=2.131036367.1374247227.1572637295-1396885475.1569306092
    //https://forum.unity.com/threads/why-is-monobehaviour-ondidapplyanimationproperties-undocumented.481600/
    private void OnDidApplyAnimationProperties()
    {
        if (faceMaterial) faceMaterial.SetTexture(faceTexturePropertyName, currentFace.texture);
    }

    private void OnEnable()
    {
        if (!faceMaterial)
        {
            Renderer renderer = GetComponentInChildren<Renderer>();

            if (Application.isPlaying)
            {
                faceMaterial = renderer.materials[faceMaterialIndex];
            }
            else
            {
                //En edit time (importante para que se pueda previsualizar la animación), utilizamos el shared material; ya que no se permite instanciar materiales en edit time.
                //Eso significa que al reproducir una animación facial en edit time, se cambia la textura asignada en el material en sí. 
                //En principio eso no debería ser un problema, pero está bien ser consciente de ello.
                faceMaterial = renderer.sharedMaterials[faceMaterialIndex];
            }
            
        }
    }
}
