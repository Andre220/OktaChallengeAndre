using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scriptable que armazena as informações físicas de cada BasicPhysicsObject
/// </summary>
[CreateAssetMenu(fileName = "CustomPhysicsProperties", menuName = "Scriptables/physics/CustomPhysicsProperties", order = 0)]
public class CustomPhysicsProperties : ScriptableObject
{
    // Se este objeto se move. Caso não se mova, testes de colisão são feitos apenas 
    // para os casos onde outros objetos colidem com ele.
    // Também previne o BasicPhysicsObject de atualizar a posição dele
    public bool isStatic; 

    // Caso seja verdadeiro, ao colidir com outro objeto, este objeto será 
    // impulsionado na direção oposta ao da colisão.
    public bool canBounce;

    // Caso seja verdadeiro, ao colidir com outro objeto, este objeto ficara 
    // parado na posicao onde colidiu nao sera impulsionado.
    public bool stopOnCollide;
}
