INSERT INTO
    structures (
        structure_id,
        structure_name,
        structure_type,
        structure_description,
        structure_image_path,
        structure_damage,
        structure_health,
        structure_cost,
        progress_level
    )
VALUES
    (
        1,
        "Turret",
        "Offensive",
        "A basic shooting turret",
        "Sprites/PlanningPhase/turretIcon",
        10,
        100,
        50,
        1
    ),
    (
        2,
        "Wall",
        "Defensive",
        "A basic wall",
        "Sprites/PlanningPhase/wallIcon",
        0,
        200,
        50,
        1
    ),
    (
        3,
        "Idol",
        "Support",
        "The idol that heals the heart",
        "Sprites/PlanningPhase/turretIcon",
        0,
        100,
        50,
        1
    ),
    (
        4,
        "FlameThrower",
        "Offensive",
        "A basic flamethrower turret",
        "Sprites/PlanningPhase/turretIcon",
        2,
        100,
        50,
        1
    ),
    (
        5,
        "FireTrap",
        "Trap",
        "A basic fire trap",
        "Sprites/PlanningPhase/turretIcon",
        2,
        100,
        50,
        1
    );