CREATE TABLE IF NOT EXISTS gameItems(
    item_id INT PRIMARY KEY,
    item_name VARCHAR(20),
    item_type VARCHAR(20),
    item_damage INT,
    item_armor INT,
    progress_level INT
);

CREATE TABLE IF NOT EXISTS structures(
    structure_id INT PRIMARY KEY,
    structure_name VARCHAR(20),
    structure_type VARCHAR(20),
    structure_damage INT,
    structure_health INT,
    structure_cost INT,
    progress_level INT
);

CREATE TABLE IF NOT EXISTS enemies(
    enemy_id INT PRIMARY KEY,
    enemy_name VARCHAR(20),
    enemy_type VARCHAR(20),
    enemy_damage INT,
    enemy_health INT,
    enemy_mana INT,
    progress_level INT
);

CREATE TABLE IF NOT EXISTS saves (
    save_id INT PRIMARY KEY,
    player_id INT,
    progress_level INT,
    FOREIGN KEY (player_id) REFERENCES players(player_id)
);

CREATE TABLE IF NOT EXISTS players (
    player_id INT PRIMARY KEY,
    player_name VARCHAR(20),
    player_health INT,
    player_mana INT
);

CREATE TABLE IF NOT EXISTS inventory (
    entry_id INT PRIMARY KEY,
    player_ID INT,
    item_ID INT,
    item_quantity INT,
    FOREIGN KEY (player_id) REFERENCES players(player_id),
    FOREIGN KEY (item_id) REFERENCES gameItems(player_id)
);