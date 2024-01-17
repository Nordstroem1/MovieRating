CREATE DATABASE	IF NOT EXISTS MovieRating;

USE movierating; 

CREATE TABLE IF NOT EXISTS Users (
	user_id INT PRIMARY KEY AUTO_INCREMENT,
    username VARCHAR(50),
    PASSWORD VARCHAR(50)
);

CREATE TABLE IF NOT EXISTS movies(
	movie_id INT PRIMARY KEY,
    title VARCHAR(50),
    description VARCHAR(250),
    genra VARCHAR(50),
    length VARCHAR(10)
);

CREATE TABLE IF NOT EXISTS review(
	movie_id INT,
    review_id INT,
    review_date DATETIME,
    user_review VARCHAR(350)
);


CREATE TABLE IF NOT EXISTS user_movies_lt(
	user_id INT,
    movie_id INT,
    PRIMARY KEY(user_id, movie_id)
);

INSERT INTO movies (movie_id, title, description, genra, length)
VALUES
(1,'The Godfather', 'The aging patriarch of an organized crime dynasty transfers control of his clandestine empire to his reluctant son.', 'Crime, Drama', '2,55'),

(2,'The Shawshank Redemption', 'Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency.', 'Drama', '2,22'),

(3,'The Dark Knight', 'When the menace known as the Joker wreaks havoc and chaos on the people of Gotham, Batman must accept one of the greatest psychological and physical tests of his ability to fight injustice.', 'Action, Crime, Drama', '2,32'),

(4,'Forrest Gump', 'The presidencies of Kennedy and Johnson, the events of Vietnam, Watergate, and other historical events unfold through the perspective of an Alabama man with an IQ of 75, whose only desire is to be reunited with his childhood sweetheart.', 'Drama, Romance', '2,22'),

(5,'The Matrix', 'A computer hacker learns from mysterious rebels about the true nature of his reality and his role in the war against its controllers.', 'Action, Sci-Fi', '2,16'),

(6,'The Silence of the Lambs', 'A young F.B.I. cadet must receive the help of an incarcerated and manipulative cannibal killer to help catch another serial killer, a madman who skins his victims.', 'Crime, Drama, Thriller', '1,58'),

(7,'The Lord of the Rings: The Fellowship of the Ring', 'A meek Hobbit from the Shire and eight companions set out on a journey to destroy the powerful One Ring and save Middle-earth from the Dark Lord Sauron.', 'Action, Adventure, Drama', '2,58'),

(8,'The Lion King', 'Lion prince Simba and his father are targeted by his bitter uncle, who wants to ascend the throne himself.', 'Animation, Adventure, Drama', '1,28'),

(9,'The Terminator', 'A human soldier is sent from 2029 to 1984 to stop an almost indestructible cyborg killing machine, sent from the same year, which has been programmed to execute a young woman whose unborn son is the key to humanity\'s future salvation.', 'Action, Sci-Fi', '1,47'),

(10,'The Shining', 'A family heads to an isolated hotel for the winter where a sinister presence influences the father into violence, while his psychic son sees horrific forebodings from both past and future.', 'Drama, Horror', '2,26'),

(11,'The Exorcist', 'When a teenage girl is possessed by a mysterious entity, her mother seeks the help of two priests to save her daughter.', 'Horror', '2,2'),

(12,'The Green Mile', 'The lives of guards on Death Row are affected by one of their charges: a black man accused of child murder and rape, yet who has a mysterious gift.', 'Crime, Drama, Fantasy', '3,09'),

(13,'The Prestige', 'After a tragic accident, two stage magicians engage in a battle to create the ultimate illusion while sacrificing everything they have to outwit each other.', 'Drama, Mystery, Sci-Fi', '2,1'),

(14,'The Departed', 'An undercover cop and a mole in the police attempt to identify each other while infiltrating an Irish gang in South Boston.', 'Crime, Drama, Thriller', '2,31'),

(15,'The Usual Suspects', 'A sole survivor tells of the twisty events leading up to a horrific gun battle on a boat, which began when five criminals met at a seemingly random police lineup.', 'Crime, Mystery, Thriller', '1,46');

CREATE INDEX genra_idx
ON movies(genra);

SHOW INDEXES FROM movies;

DROP PROCEDURE insert_user;
-- funktion för att sätta in en användare--
DELIMITER $$
CREATE PROCEDURE insert_user(
    username VARCHAR(50),
    PASSWORD VARCHAR(50)
)
BEGIN
	INSERT INTO users VALUES (DEFAULT, 
							  username, 
                              PASSWORD);
END$$
DELIMITER ;

SELECT * FROM users;

SET sql_safe_updates = 1;

CREATE VIEW user_movies AS
SELECT *
FROM Users
JOIN user_movie_lt ON Users.user_id = user_movie_lt.user_id
JOIN movies ON movies.movie_id = user_movie_lt.movie_id;

SELECT * FROM user_movies;