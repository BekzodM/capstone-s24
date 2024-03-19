INSERT INTO
    structureUpgrades (
        upgrade_id,
        upgrade_name,
        upgrade_description,
        upgrade_image_path,
        upgrade_slot,
        upgrade_cost,
        next_upgrade_id,
        structure_id
    )
VALUES
-- Turret Upgrades
    -- Turret Upgrade Slot 0
    (1, "Lv.1 More Damage!", "Each shot does 20 damage", "image_path", 0, 50, 2, 1),
    (2, "Lv.2 More Damage!", "Each shot does 20 damage", "image_path", 0, 50, 3, 1),
    (3, "Lv.3 More Damage!", "Each shot does 20 damage", "image_path", 0, 50, 4, 1),
    (4, "Lv.4 More Damage!", "Each shot does 20 damage", "image_path", 0, 50, 5, 1),
    (5, "Lv.5 More Damage!", "Each shot does 20 damage", "image_path", 0, 50, 0, 1),
    -- Turret Upgrade Slot 1
    (6, "Lv.1 More Speed!", "Turret shoots 0.5 times faster", "image_path", 1, 50, 7, 1),
    (7, "Lv.2 More Speed!", "Turret shoots 0.5 times faster", "image_path", 1, 50, 8, 1),
    (8, "Lv.3 More Speed!", "Turret shoots 0.5 times faster", "image_path", 1, 50, 9, 1),
    (9, "Lv.4 More Speed!", "Turret shoots 0.5 times faster", "image_path", 1, 50, 10, 1),
    (10, "Lv.5 More Speed!", "Turret shoots 0.5 times faster", "image_path", 1, 50, 0, 1),
    -- Turret Upgrade Slot 2
    (11, "Lv.1 More Range!", "Turret range is 0.5 bigger", "image_path", 2, 50, 12, 1),
    (12, "Lv.2 More Range!", "Turret range is 0.5 bigger", "image_path", 2, 50, 13, 1),
    (13, "Lv.3 More Range!", "Turret range is 0.5 bigger", "image_path", 2, 50, 14, 1),
    (14, "Lv.4 More Range!", "Turret range is 0.5 bigger", "image_path", 2, 50, 15, 1),
    (15, "Lv.5 More Range!", "Turret range is 0.5 bigger", "image_path", 2, 50, 0, 1),
-- Wall Upgrades
    -- Wall Upgrade Slot 0
    (16, "Lv.1 More Health!", "Health increase by 100", "image_path", 0, 50, 17, 2),
    (17, "Lv.2 More Health!", "Health increase by 100", "image_path", 0, 50, 18, 2),
    (18, "Lv.3 More Health!", "Health increase by 100", "image_path", 0, 50, 19, 2),
    (19, "Lv.4 More Health!", "Health increase by 100", "image_path", 0, 50, 20, 2),
    (20, "Lv.5 More Health!", "Health increase by 100", "image_path", 0, 50, 0, 2),
    -- Wall Upgrade Slot 1
    (21, "Lv.1 More Bigger Wall!", "Wall size is 0.5 bigger", "image_path", 0, 50, 22, 2),
    (22, "Lv.2 More Bigger Wall!", "Wall size is 0.5 bigger", "image_path", 0, 50, 23, 2),
    (23, "Lv.3 More Bigger Wall!", "Wall size is 0.5 bigger", "image_path", 0, 50, 24, 2),
    (24, "Lv.4 More Bigger Wall!", "Wall size is 0.5 bigger", "image_path", 0, 50, 25, 2),
    (25, "Lv.5 More Bigger Wall!", "Wall size is 0.5 bigger", "image_path", 0, 50, 0, 2),
    -- Wall Upgrade Slot 2
    (26, "Lv.1 Wall Turrets", "Add a Mini Turret to the walls. Mini Turrets deal 5 damage per shot.", "image_path", 0, 50, 27, 2),
    (27, "Lv.2 Another Mini Turret", "Add another Mini Turret to the wall. Mini Turrets deal 5 damage per shot.", "image_path", 0, 50, 28, 2),
    (28, "Lv.3 Stronger Wall Turrets", "Mini Turrets deal 10 damage per shot.", "image_path", 0, 50, 29, 2),
    (29, "Lv.4 Electrified Turrets!", "Enemies that are shot with the Mini Turrets electrify nearby enemies.", "image_path", 0, 50, 30, 2),
    (30, "Lv.5 Slow Down!", "Electrified enemies move slower for 5 seconds.", "image_path", 0, 50, 0, 2)
    ;
