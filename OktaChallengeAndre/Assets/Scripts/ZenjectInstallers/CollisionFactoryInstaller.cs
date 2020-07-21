using UnityEngine;
using Zenject;

/// <summary>
/// 
/// Responsavel por pegar todos os locais que solicitam a injecao de uma classe que 
/// implementa ICustomFactory e injetar a classe CustomCollision.
/// O AsSingle garante que sera injetado a mesma instancia em todos os locais. 
/// Isso e importante aqui pois o CustomCollision armazena a lista de objetos "colidiveis" 
/// na cena. Caso nao houvesse apenas uma istancia, teriamos diversas listas armazenando a mesma informacao.
/// Poderia ser um singleton, mas com isso, teriamos que referenciar a classe CustomCollision em todos
/// os scripts que fossem a utilizar, gerando um acoplamento forte entre as classes.
/// </summary>
public class CollisionFactoryInstaller : MonoInstaller<CollisionFactoryInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<ICustomCollision>().To<CustomCollision>().AsSingle();
    }
}