No tocar configuración de componentes si esto no dice que lo haga.
No tocar los AnimationControllers. Las animaciones de cada Towersona se asignan mediante AnimationControllerOverrides.

- El prefab ya existe, no crear uno nuevo. (Cat HOD LVL 1, Dragon HOD LVL 3 - 1...)
    - Si entras a él, tiene un hijo que es otro prefab (el modelo). Si hay que sustituir el modelo, sustituir ESTE prefab hijo, no el padre.

- Por defecto, todos los objetos en layer DetailedTowersona. Si asignas el layer al objeto padre, te ofrece poner todos los hijos al mismo layer.
- Poner al menos un Caressable con Collidercillo (la forma que quieras), en CaressableLayer. (En un GameObject hijo del hueso que siga, no en el hueso en sí.)
- Al menos un Feedable con su Collidercillo (la forma que quieras) en FeedableLayer. (En un GameObject hijo del hueso que siga, no en el hueso en sí.)
- No preocuparse por Is Trigger. Se ajusta automáticamente al jugar. Ignorar también Physics Material.

- El Animator que se pone por defecto en el root del modelo. ESTE ES EL ANIMATOR PARA EL CUERPO.
    - Preset: pre_HODAnimator.
    - Asignar el avatar adecuado (se importa junto al modelo).
    - Asignar un Animation Controller Override de ac_HOD_Body con las animaciones correctas asignadas.

- Un FaceAnimator en el GameObject que tiene el SkinnedMeshRenderer. Creará un Animator automáticamente. ESTE ES EL ANIMATOR PARA LA CARA.
    - Preset para el Animator: pre_HODAnimator.
    - No asignar ningún avatar al Animator.
    - Utilizar un Animation Controller Override de ac_HOD_Face y asignar las animaciones faciales correctas.
    - FaceAnimator viene ya configurado.

- Configuración del SkinnedMeshRenderer: (lo que no diga aquí, dejar el valor por defecto).
    - Light Probes: Off
    - Blend Probes: Off
    - Cast shadows: On
    - Receive shadows: On
    - Asignar materiales de cuerpo y cara en el Renderer si no están ya.
    - Asegúrate de que estén el orden correcto. Asignar el índice del material de la cara en "FaceMaterialIndex" en FaceAnimator (1 por defecto).
    - Dynamic occluded: On

- Asignar las referencias que estén vacías en el root (referencia a la cabeza en LookAtFood etc.). Aparecen con un mensaje de error hasta que les asignes la referencia.
    - ProceduralSleepAnimation, LookAtFood y ProceduralCaressAnimation tienen parámetros de animación que se pueden tocar al gusto.
    - El prefab es un variante de un prefab base para todas las towersonas. Si algún parámetro se quiere cambiar para todas las torres, tócala en _TowersonaHODBase.