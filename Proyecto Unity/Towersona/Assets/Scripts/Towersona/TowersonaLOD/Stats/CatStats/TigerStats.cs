using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "Stats/Cat/Tiger")]
public class TigerStats : ScriptableObject
{
    public Vector2 dañoBala;
    public Vector2 velocidadDeAtaque;
    public Vector2 rango;
    public Vector2 velocidadBala;

    public Vector2 dineroPorSegundo;
    public Vector2 tamañoAreaDañoBala;

    [Tooltip("Cuanta felicidad gana la towersona por caricia")]
    public float felicidadPorCaricia;
    [Tooltip("Hambre que pierde por segundo la towersona")]
    public float hambrePorSegundo;
    [Tooltip("Amor que pierde por segundo la towersona")]
    public float perdidaFelicidadPorSegundo;
    [Tooltip("Cuanto cae el amor por cada mierda que hay sin recoger")]
    public float enfadoPorMierda;
    public float tiempoEntreMierdas;
    public int maxMierdas;
}
