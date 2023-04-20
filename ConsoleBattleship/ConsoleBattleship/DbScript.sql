DROP TABLE users;
DROP TABLE games;

CREATE TABLE users (
   user_id INTEGER PRIMARY KEY,
   username TEXT NOT NULL,
   wins INTEGER DEFAULT 0,
   losses INTEGER DEFAULT 0
);

CREATE TABLE games (
  game_id INTEGER,
  user_id INTEGER,
  score INTEGER NOT NULL,
  FOREIGN KEY (user_id) REFERENCES users(user_id),
  PRIMARY KEY (game_id,user_ID)
);

INSERT INTO users (username) VALUES ("NicholasJ");
