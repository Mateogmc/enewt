# ENEWT

## Proyecto de fin de ciclo de Desarrollo de Aplicaciones Multiplataforma

### Tipos de tanques

#### *V1AI:* Warden
- *Tipo de movimiento:* Estático
- *Velocidad del proyectil:* Lento
- *Cantidad de proyectiles:* 1
- *Cadencia de tiro:* Rápida
- *Rebotes máximos de los proyectiles:* 1

- *Comportamiento:* Se mantiene estático, apuntando en dirección al jugador. Disparará en cuanto exista contacto visual.

#### *V2AI*: Seeker
- *Tipo de movimiento:* Lento
- *Velocidad del proyectil:* Lento
- *Cantidad de proyectiles:* 1
- *Cadencia de tiro:* Lenta
- *Rebotes máximos de los proyectiles:* 2

- *Comportamiento:* Seguirá al jugador por el camino más corto esquivando obstáculos. En cuanto exista contacto visual, esperará unos segundos, y si sigue existiendo contacto visual, disparará.

#### *V3AI*: Barrager
- *Tipo de movimiento:* Rápido
- *Velocidad del proyectil:* Lento
- *Cantidad de proyectiles:* 5
- *Cadencia de tiro:* Muy rápida
- *Rebotes máximos de los proyectiles:* 1

- *Comportamiento:* Seguirá al jugador por el camino más corto esquivando obstáculos hasta que esté a cierta distancia y exista línea de visión. En cuanto se cumplan esas condiciones, se empezará a mover en paralelo a la dirección al jugador y tras unas fracciones de segundo mientras exista línea de visión disparará una ráfaga de proyectiles.

#### *V4AI*: Sniper
- *Tipo de movimiento:* Estático
- *Velocidad del proyectil:* Rápido
- *Cantidad de proyectiles:* 1
- *Cadencia de tiro:* Muy lenta
- *Rebotes máximos de los proyectiles:* 3

- *Comportamiento:* Se mantiene estático, barriendo todo su alrededor con el cañón. Disparará proyectiles muy rápidos en cuanto exista contacto visual, su línea de visión incluye los rebotes de los proyectiles en las paredes, y podrá disparar tanto al jugador como a la futura posición del jugador si se está moviendo.

#### ¿*V5AI*: Brute?
- *Tipo de movimiento:* Lento
- *Velocidad del proyectil:* Muy rápido
- *Cantidad de proyectiles:* 1
- *Cadencia de tiro:* Rápida
- *Rebotes máximos de los proyectiles:* 0

- *Comportamiento:* Avanza lentamente en dirección al jugador esquivando obstáculos. En cuanto exista contacto visual, esperará unos segundos, y si sigue existiendo contacto visual, disparará proyectiles extremadamente rápidos pero que no rebotan.
