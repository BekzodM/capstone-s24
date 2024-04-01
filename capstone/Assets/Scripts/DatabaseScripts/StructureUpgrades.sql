INSERT INTO
    structureUpgrades (
        upgrade_id,
        upgrade_name,
        upgrade_description,
        upgrade_image_path,
        upgrade_slot,
        upgrade_cost,
        structure_id
    )
VALUES
-- Turret Upgrades
    -- Turret Upgrade Slot 0
    (1, "Lv.1 More Damage!", "Each shot does 20 damage", "Sprites/PlanningPhase/attackIcon", 0, 50, 1),
    (2, "Lv.2 More Damage!", "Each shot does 20 damage", "Sprites/PlanningPhase/attackIcon", 0, 50, 1),
    (3, "Lv.3 More Damage!", "Each shot does 20 damage", "Sprites/PlanningPhase/attackIcon", 0, 50, 1),
    (4, "Lv.4 More Damage!", "Each shot does 20 damage", "Sprites/PlanningPhase/attackIcon", 0, 50, 1),
    (5, "Lv.5 More Damage!", "Each shot does 20 damage", "Sprites/PlanningPhase/attackIcon", 0, 50, 1),
    -- Turret Upgrade Slot 1
    (6, "Lv.1 More Speed!", "Turret shoots 0.5 times faster", "Sprites/PlanningPhase/attackIcon", 1, 50, 1),
    (7, "Lv.2 More Speed!", "Turret shoots 0.5 times faster", "Sprites/PlanningPhase/attackIcon", 1, 50, 1),
    (8, "Lv.3 More Speed!", "Turret shoots 0.5 times faster", "Sprites/PlanningPhase/attackIcon", 1, 50, 1),
    (9, "Lv.4 More Speed!", "Turret shoots 0.5 times faster", "Sprites/PlanningPhase/attackIcon", 1, 50, 1),
    (10, "Lv.5 More Speed!", "Turret shoots 0.5 times faster", "Sprites/PlanningPhase/attackIcon", 1, 50, 1),
    -- Turret Upgrade Slot 2
    (11, "Lv.1 More Range!", "Turret range is 0.5 bigger", "Sprites/PlanningPhase/attackIcon", 2, 50, 1),
    (12, "Lv.2 More Range!", "Turret range is 0.5 bigger", "Sprites/PlanningPhase/attackIcon", 2, 50, 1),
    (13, "Lv.3 More Range!", "Turret range is 0.5 bigger", "Sprites/PlanningPhase/attackIcon", 2, 50, 1),
    (14, "Lv.4 More Range!", "Turret range is 0.5 bigger", "Sprites/PlanningPhase/attackIcon", 2, 50, 1),
    (15, "Lv.5 More Range!", "Turret range is 0.5 bigger", "Sprites/PlanningPhase/attackIcon", 2, 50, 1),
-- Wall Upgrades
    -- Wall Upgrade Slot 0
    (16, "Lv.1 More Health!", "Health increase by 100", "Sprites/PlanningPhase/attackIcon", 0, 50, 2),
    (17, "Lv.2 More Health!", "Health increase by 100", "Sprites/PlanningPhase/attackIcon", 0, 50, 2),
    (18, "Lv.3 More Health!", "Health increase by 100", "Sprites/PlanningPhase/attackIcon", 0, 50, 2),
    (19, "Lv.4 More Health!", "Health increase by 100", "Sprites/PlanningPhase/attackIcon", 0, 50, 2),
    (20, "Lv.5 The Ultimate Defense!", "Health increase by 100", "Sprites/PlanningPhase/attackIcon", 0, 50, 2),
    -- Wall Upgrade Slot 1
    (21, "Lv.1 More Bigger Wall!", "Wall size is 0.5 bigger", "Sprites/PlanningPhase/attackIcon", 0, 50, 2),
    (22, "Lv.2 More Bigger Wall!", "Wall size is 0.5 bigger", "Sprites/PlanningPhase/attackIcon", 0, 50, 2),
    (23, "Lv.3 More Bigger Wall!", "Wall size is 0.5 bigger", "Sprites/PlanningPhase/attackIcon", 0, 50, 2),
    (24, "Lv.4 More Bigger Wall!", "Wall size is 0.5 bigger", "Sprites/PlanningPhase/attackIcon", 0, 50, 2),
    (25, "Lv.5 The Biggest Wall!", "Wall size is 0.5 bigger", "Sprites/PlanningPhase/attackIcon", 0, 50, 2),
    -- Wall Upgrade Slot 2
    (26, "Lv.1 Firewall", "Each time an enemy touches the wall, they take 5 damage.", "Sprites/PlanningPhase/attackIcon", 0, 50, 2),
    (27, "Lv.2 Firewall", "Each time an enemy touches the wall, they take 10 damage.", "Sprites/PlanningPhase/attackIcon", 0, 50, 2),
    (28, "Lv.3 Firewall", "Each time an enemy touches the wall, they take 15 damage.", "Sprites/PlanningPhase/attackIcon", 0, 50, 2),
    (29, "Lv.4 Firewall", "Each time an enemy touches the wall, they take 20 damage.", "Sprites/PlanningPhase/attackIcon", 0, 50, 2),
    (30, "Lv.5 That's a lot of heat!", "Each time an enemy touches the wall, they take 25 damage.", "Sprites/PlanningPhase/attackIcon", 0, 50, 2)
    ;
