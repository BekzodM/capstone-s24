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
    structure_description VARCHAR(255),
    structure_damage INTEGER,
    structure_health INTEGER,
    structure_cost INTEGER,
    progress_level INTEGER
);

CREATE TABLE IF NOT EXISTS structureUpgrades(
    upgrade_id INTEGER NOT NULL PRIMARY KEY,
    upgrade_name VARCHAR(20),
    upgrade_type VARCHAR(20),
    upgrade_description VARCHAR(255),
    upgrade_slot INTEGER
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