CREATE TABLE IF NOT EXISTS structures(
    structure_id INTEGER NOT NULL PRIMARY KEY,
    structure_name VARCHAR(20),
    structure_type VARCHAR(20),
    structure_description VARCHAR(255),
    structure_image_path VARCHAR(255),
    structure_damage INTEGER,
    structure_health INTEGER,
    structure_cost INTEGER,
    progress_level INTEGER
);

CREATE TABLE IF NOT EXISTS structureUpgrades(
    upgrade_id INTEGER NOT NULL PRIMARY KEY,
    upgrade_name VARCHAR(20),
    upgrade_description VARCHAR(255),
    upgrade_image_path VARCHAR(255),
    upgrade_slot INTEGER,
    upgrade_cost INTEGER,
    structure_id INTEGER,
    FOREIGN KEY (structure_id) REFERENCES structures(structure_id)
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