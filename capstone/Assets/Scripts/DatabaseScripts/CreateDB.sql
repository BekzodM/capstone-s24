CREATE TABLE IF NOT EXISTS gameItems(
    item_id INTEGER NOT NULL PRIMARY KEY,
    item_name VARCHAR(20),
    item_type VARCHAR(20),
    item_damage INTEGER,
    item_armor INTEGER,
    progress_level INTEGER
);

CREATE TABLE IF NOT EXISTS structures(
    structure_id INTEGER NOT NULL PRIMARY KEY,
    structure_name VARCHAR(20),
    structure_type VARCHAR(20),
    structure_damage INTEGER,
    structure_health INTEGER,
    structure_cost INTEGER,
    progress_level INTEGER
);

CREATE TABLE IF NOT EXISTS enemies(
    enemy_id INTEGER NOT NULL PRIMARY KEY,
    enemy_name VARCHAR(20),
    enemy_type VARCHAR(20),
    enemy_damage INTEGER,
    enemy_health INTEGER,
    enemy_mana INTEGER,
    progress_level INTEGER
);

CREATE TABLE IF NOT EXISTS saves (
    save_id INTEGER NOT NULL PRIMARY KEY,
    slot_id INTEGER UNIQUE,
    player_id INTEGER,
    progress_level INTEGER,
    FOREIGN KEY (player_id) REFERENCES players(player_id)
);

CREATE TABLE IF NOT EXISTS players (
    player_id INTEGER NOT NULL PRIMARY KEY,
    player_name VARCHAR(20),
    player_type VARCHAR(20),
    player_health INTEGER,
    player_mana INTEGER
);

CREATE TABLE IF NOT EXISTS inventory (
    entry_id INTEGER NOT NULL PRIMARY KEY,
    player_id INTEGER,
    item_id INTEGER,
    item_quantity INTEGER,
    FOREIGN KEY (player_id) REFERENCES players(player_id),
    FOREIGN KEY (item_id) REFERENCES gameItems(player_id)
);

-- Dummy Insert Data --
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
    (1, 'Sword', 'Weapon', 10, 0, 1),
    (2, 'Shield', 'Armor', 0, 20, 2),
    (3, 'Potion', 'Consumable', 0, 0, 3);

INSERT INTO
    structures (
        structure_id,
        structure_name,
        structure_type,
        structure_damage,
        structure_health,
        structure_cost,
        progress_level
    )
VALUES
    (1, 'Cannon', 'Offense', 0, 100, 1000, 1),
    (2, 'Wall', 'Defense', 0, 50, 500, 2),
    (3, 'Turret', 'Offense', 20, 80, 800, 3);

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
    (1, 'Goblin', 'Melee', 15, 50, 0, 1),
    (2, 'Skeleton', 'Undead', 20, 70, 0, 2),
    (3, 'Mage', 'Magic', 25, 60, 100, 3);

INSERT INTO
    saves (save_id, slot_id, player_id, progress_level)
VALUES
    (1, 1, 1, 10),
    (2, 2, 2, 5),
    (3, 3, 3, 15);

INSERT INTO
    players (
        player_id,
        player_name,
        player_type,
        player_health,
        player_mana
    )
VALUES
    (1, 'John', 'Warrior', 100, 50),
    (2, 'Alice', 'Rogue', 80, 70),
    (3, 'Bob', 'Mage', 60, 100);

INSERT INTO
    inventory (entry_id, player_id, item_id, item_quantity)
VALUES
    (1, 1, 1, 1),
    (2, 2, 2, 2),
    (3, 3, 3, 3);