using GameEventBus.Events;

/// <summary>
/// 
/// Evento de colisao entre dois objetos. Repare que ele recebe 
/// dois BasicPhysicsObject, que podem ser tanto um circulo quanto um quadrado,
/// permintindo que este evento possa retornar qualquer tipo de colisao
/// (circulo/circulo ou circulo/quadrado ou quadrado/quadrado)
/// 
/// Exemplo em https://github.com/ThomasKomarnicki/GameEventBus
/// 
/// </summary>

public class OnPhysicsObjectCollide : EventBase
{
    public BasicPhysicsObject Collider;
    public BasicPhysicsObject Collided;

    public OnPhysicsObjectCollide(BasicPhysicsObject collider, BasicPhysicsObject collided)
    {
        Collider = collider;
        Collided = collided;
    }
}
