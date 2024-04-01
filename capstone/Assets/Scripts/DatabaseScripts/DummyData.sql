-- Inserting sample game items
INSERT INTO
    gameItems (
        item_id,
        item_name,
        item_type,
        item_damage,
        item_armor,
        progress_level
    )
VALUES
    (1, 'Health Potion', 'Consumable', 0, 0, 1),
    (2, 'Mana Potion', 'Consumable', 0, 0, 1);

-- Inserting sample structures
INSERT INTO
    structures (
        structure_id,
        structure_name,
        structure_type,
        structure_description,
        structure_damage,
        structure_health,
        structure_cost,
        upgrade_id,
        progress_level
    )
VALUES
    (
        1,
        'Arrow Tower',
        'Defense',
        'Basic tower shooting arrows.',
        20,
        100,
        100,
        NULL,
        1
    ),
    (
        2,
        'Cannon Tower',
        'Defense',
        'Powerful tower shooting cannonballs.',
        50,
        150,
        200,
        NULL,
        2
    );

-- Inserting sample structure upgrades
INSERT INTO
    structureUpgrades (
        upgrade_id,
        upgrade_name,
        upgrade_description,
        upgrade_image_path,
        upgrade_slot
    )
VALUES
    (
        1,
        'Arrow Tower Upgrade',
        'Increases damage and range of the arrow tower.',
        '/images/arrow_upgrade.png',
        1
    ),
    (
        2,
        'Cannon Tower Upgrade',
        'Increases damage and health of the cannon tower.',
        '/images/cannon_upgrade.png',
        1
    );

-- Inserting sample enemies
INSERT INTO
    enemies (
        enemy_id,
        enemy_name,
        enemy_type,
        enemy_damage,
        enemy_health,
        enemy_mana,
        progress_level
    )
VALUES
    (1, 'Goblin', 'Ground', 10, 50, 0, 1),
    (2, 'Wyvern', 'Air', 20, 100, 0, 2);

-- Inserting sample players
INSERT INTO
    players (
        player_id,
        player_name,
        player_type,
        player_health,
        player_mana
    )
VALUES
    (1, 'John', 'Fighter', 100, 100),
    (2, 'Alice', 'Mage', 150, 80);

-- Inserting sample saves
INSERT INTO
    saves (save_id, player_id, progress_level)
VALUES
    (1, 1, 5),
    (2, 2, 3);