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
    (1, "Lv.1 More Damage!", "Each shot does 20 damage", "Sprites/PlanningPhase/turretIconMoreDamage", 0, 50, 1),
    (2, "Lv.2 More Damage!", "Each shot does 20 damage", "Sprites/PlanningPhase/turretIconMoreDamage", 0, 50, 1),
    (3, "Lv.3 More Damage!", "Each shot does 20 damage", "Sprites/PlanningPhase/turretIconMoreDamage", 0, 50, 1),
    (4, "Lv.4 More Damage!", "Each shot does 20 damage", "Sprites/PlanningPhase/turretIconMoreDamage", 0, 50, 1),
    (5, "Lv.5 More Damage!", "Each shot does 20 damage", "Sprites/PlanningPhase/turretIconMoreDamage", 0, 50, 1),
    -- Turret Upgrade Slot 1
    (6, "Lv.1 More Speed!", "Turret shoots 0.5x faster", "Sprites/PlanningPhase/turretIconMoreSpeed", 1, 50, 1),
    (7, "Lv.2 More Speed!", "Turret shoots 0.5x faster", "Sprites/PlanningPhase/turretIconMoreSpeed", 1, 50, 1),
    (8, "Lv.3 More Speed!", "Turret shoots 0.5x faster", "Sprites/PlanningPhase/turretIconMoreSpeed", 1, 50, 1),
    (9, "Lv.4 More Speed!", "Turret shoots 0.5x faster", "Sprites/PlanningPhase/turretIconMoreSpeed", 1, 50, 1),
    (10, "Lv.5 More Speed!", "Turret shoots 0.5x faster", "Sprites/PlanningPhase/turretIconMoreSpeed", 1, 50, 1),
    -- Turret Upgrade Slot 2
    (11, "Lv.1 More Range!", "Turret range is 0.5 bigger", "Sprites/PlanningPhase/turretIconMoreRange", 2, 50, 1),
    (12, "Lv.2 More Range!", "Turret range is 0.5 bigger", "Sprites/PlanningPhase/turretIconMoreRange", 2, 50, 1),
    (13, "Lv.3 More Range!", "Turret range is 0.5 bigger", "Sprites/PlanningPhase/turretIconMoreRange", 2, 50, 1),
    (14, "Lv.4 More Range!", "Turret range is 0.5 bigger", "Sprites/PlanningPhase/turretIconMoreRange", 2, 50, 1),
    (15, "Lv.5 More Range!", "Turret range is 0.5 bigger", "Sprites/PlanningPhase/turretIconMoreRange", 2, 50, 1),
-- Wall Upgrades
    -- Wall Upgrade Slot 0
    (16, "Lv.1 More Health!", "Health increase by 100", "Sprites/PlanningPhase/wallIconMoreHealth", 0, 50, 2),
    (17, "Lv.2 More Health!", "Health increase by 100", "Sprites/PlanningPhase/wallIconMoreHealth", 0, 50, 2),
    (18, "Lv.3 More Health!", "Health increase by 100", "Sprites/PlanningPhase/wallIconMoreHealth", 0, 50, 2),
    (19, "Lv.4 More Health!", "Health increase by 100", "Sprites/PlanningPhase/wallIconMoreHealth", 0, 50, 2),
    (20, "Lv.5 The Ultimate Defense!", "Health increase by 100", "Sprites/PlanningPhase/wallIconMoreHealth", 0, 50, 2),
    -- Wall Upgrade Slot 1
    (21, "Lv.1 More Bigger Wall!", "Wall size is 0.2x bigger", "Sprites/PlanningPhase/wallIconBiggerWall", 0, 50, 2),
    (22, "Lv.2 More Bigger Wall!", "Wall size is 0.2x bigger", "Sprites/PlanningPhase/wallIconBiggerWall", 0, 50, 2),
    (23, "Lv.3 More Bigger Wall!", "Wall size is 0.2x bigger", "Sprites/PlanningPhase/wallIconBiggerWall", 0, 50, 2),
    (24, "Lv.4 More Bigger Wall!", "Wall size is 0.2x bigger", "Sprites/PlanningPhase/wallIconBiggerWall", 0, 50, 2),
    (25, "Lv.5 The Biggest Wall!", "Wall size is 0.2x bigger", "Sprites/PlanningPhase/wallIconBiggerWall", 0, 50, 2),
    -- Wall Upgrade Slot 2
    (26, "Lv.1 Firewall", "Each time an enemy touches the wall, they take 5 damage.", "Sprites/PlanningPhase/wallIconFirewall", 0, 50, 2),
    (27, "Lv.2 Firewall", "Each time an enemy touches the wall, they take 10 damage.", "Sprites/PlanningPhase/wallIconFirewall", 0, 50, 2),
    (28, "Lv.3 Firewall", "Each time an enemy touches the wall, they take 15 damage.", "Sprites/PlanningPhase/wallIconFirewall", 0, 50, 2),
    (29, "Lv.4 Firewall", "Each time an enemy touches the wall, they take 20 damage.", "Sprites/PlanningPhase/wallIconFirewall", 0, 50, 2),
    (30, "Lv.5 That's a lot of heat!", "Each time an enemy touches the wall, they tae 25 damage.", "Sprites/PlanningPhase/wallIconFirewall", 0, 50, 2),
-- Idol Upgrades
    --Idol Upgrade Slot 0
    (31, "Lv.1 Healing Aura", "All allies within range, heal 2 health every 10 seconds.", "Sprites/PlanningPhase/wallIconFirewall", 0, 50, 3),
    (32, "Lv.2 Healing Aura", "All allies within range, heal 3 health every 10 seconds.", "Sprites/PlanningPhase/wallIconFirewall", 0, 50, 3),
    (33, "Lv.3 Healing Aura", "All allies within range, heal 4 health every 10 seconds.", "Sprites/PlanningPhase/wallIconFirewall", 0, 50, 3),
    (34, "Lv.4 Healing Aura", "All allies within range, heal 5 health every 10 seconds.", "Sprites/PlanningPhase/wallIconFirewall", 0, 50, 3),
    (35, "Lv.5 Healing Aura", "All allies within range, heal 6 health every 10 seconds.", "Sprites/PlanningPhase/wallIconFirewall", 0, 50, 3),
    --Idol Upgrade Slot 1
    (36, "Lv.1 Faster Healing", "Heals 0.5x faster.", "Sprites/PlanningPhase/wallIconFirewall", 0, 50, 3),
    (37, "Lv.2 Faster Healing", "Heals 0.5x faster.", "Sprites/PlanningPhase/wallIconFirewall", 0, 50, 3),
    (38, "Lv.3 Faster Healing", "Heals 0.5x faster.", "Sprites/PlanningPhase/wallIconFirewall", 0, 50, 3),
    (39, "Lv.4 Faster Healing", "Heals 0.5x faster.", "Sprites/PlanningPhase/wallIconFirewall", 0, 50, 3),
    (40, "Lv.5 Faster Healing", "Heals 0.5x faster.", "Sprites/PlanningPhase/wallIconFirewall", 0, 50, 3),
    --Idol Upgrade Slot 2
    (41, "Lv.1 Bigger Aura", "Healing range is 0.5x bigger.", "Sprites/PlanningPhase/wallIconFirewall", 0, 50, 3),
    (42, "Lv.2 Bigger Aura", "Healing range is 0.5x bigger.", "Sprites/PlanningPhase/wallIconFirewall", 0, 50, 3),
    (43, "Lv.3 Bigger Aura", "Healing range is 0.5x bigger.", "Sprites/PlanningPhase/wallIconFirewall", 0, 50, 3),
    (44, "Lv.4 Bigger Aura", "Healing range is 0.5x bigger.", "Sprites/PlanningPhase/wallIconFirewall", 0, 50, 3),
    (45, "Lv.5 Bigger Aura", "Healing range is 0.5x bigger.", "Sprites/PlanningPhase/wallIconFirewall", 0, 50, 3)
    ;
