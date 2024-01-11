CREATE DATABASE	IF NOT EXISTS MovieRating;

USE movierating; 

CREATE TABLE IF NOT EXISTS Users (
	user_id INT PRIMARY KEY AUTO_INCREMENT,
    username VARCHAR(50),
    PASSWORD VARCHAR(50)
);

CREATE TABLE IF NOT EXISTS movies(
	movie_id INT PRIMARY KEY AUTO_INCREMENT,
    title VARCHAR(50),
    description VARCHAR(250),
    genra VARCHAR(50),
    length VARCHAR(10),
    rating varchar(2)
);

CREATE TABLE IF NOT EXISTS review(
	movie_id INT,
    review_date DATETIME,
    user_review VARCHAR(350)
);

CREATE TABLE IF NOT EXISTS user_movies(
	user_id INT PRIMARY KEY ,
    movie_id INT PRIMARY KEY
);

INSERT INTO movies (title, description, genra, length, rating)
VALUES
('The Godfather', 'The aging patriarch of an organized crime dynasty transfers control of his clandestine empire to his reluctant son.', 'Crime, Drama', '2,55', NULL),

('The Shawshank Redemption', 'Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency.', 'Drama', '2,22', NULL),

('The Dark Knight', 'When the menace known as the Joker wreaks havoc and chaos on the people of Gotham, Batman must accept one of the greatest psychological and physical tests of his ability to fight injustice.', 'Action, Crime, Drama', '2,32', NULL),

('Forrest Gump', 'The presidencies of Kennedy and Johnson, the events of Vietnam, Watergate, and other historical events unfold through the perspective of an Alabama man with an IQ of 75, whose only desire is to be reunited with his childhood sweetheart.', 'Drama, Romance', '2,22', NULL),

('The Matrix', 'A computer hacker learns from mysterious rebels about the true nature of his reality and his role in the war against its controllers.', 'Action, Sci-Fi', '2,16', NULL),

('The Silence of the Lambs', 'A young F.B.I. cadet must receive the help of an incarcerated and manipulative cannibal killer to help catch another serial killer, a madman who skins his victims.', 'Crime, Drama, Thriller', '1,58', NULL),

('The Lord of the Rings: The Fellowship of the Ring', 'A meek Hobbit from the Shire and eight companions set out on a journey to destroy the powerful One Ring and save Middle-earth from the Dark Lord Sauron.', 'Action, Adventure, Drama', '2,58', NULL),

('The Lion King', 'Lion prince Simba and his father are targeted by his bitter uncle, who wants to ascend the throne himself.', 'Animation, Adventure, Drama', '1,28', NULL),

('The Terminator', 'A human soldier is sent from 2029 to 1984 to stop an almost indestructible cyborg killing machine, sent from the same year, which has been programmed to execute a young woman whose unborn son is the key to humanity\'s future salvation.', 'Action, Sci-Fi', '1,47', NULL),

('The Shining', 'A family heads to an isolated hotel for the winter where a sinister presence influences the father into violence, while his psychic son sees horrific forebodings from both past and future.', 'Drama, Horror', '2,26', NULL),

('The Exorcist', 'When a teenage girl is possessed by a mysterious entity, her mother seeks the help of two priests to save her daughter.', 'Horror', '2,2', NULL),

('The Green Mile', 'The lives of guards on Death Row are affected by one of their charges: a black man accused of child murder and rape, yet who has a mysterious gift.', 'Crime, Drama, Fantasy', '3,09', NULL),

('The Prestige', 'After a tragic accident, two stage magicians engage in a battle to create the ultimate illusion while sacrificing everything they have to outwit each other.', 'Drama, Mystery, Sci-Fi', '2,1', NULL),

('The Departed', 'An undercover cop and a mole in the police attempt to identify each other while infiltrating an Irish gang in South Boston.', 'Crime, Drama, Thriller', '2,31', NULL),

('The Usual Suspects', 'A sole survivor tells of the twisty events leading up to a horrific gun battle on a boat, which began when five criminals met at a seemingly random police lineup.', 'Crime, Mystery, Thriller', '1,46', NULL);
