
DROP TABLE itinerary_landmark;
DROP TABLE itinerary;
DROP TABLE review;
DROP TABLE app_user;
DROP TABLE landmark;

CREATE TABLE landmark (
id INTEGER IDENTITY(1,1) not null,
image_name VARCHAR(max) not null,
landmark_description VARCHAR(max) not null,
name VARCHAR(max) not null,
admin_approved BIT not null,
longitude float not null,
latitude float not null,
google_api_placeID VARCHAR(200) not null,
CONSTRAINT uc_API_Reference UNIQUE (google_api_placeID),
CONSTRAINT pk_landmark_id PRIMARY KEY (id)
);

--CREATE TABLE landmark (
--id INTEGER IDENTITY(1,1) not null,
--image_name VARCHAR(200) not null,
--landmark_description VARCHAR(8000) not null,
--name VARCHAR(64) not null,
--admin_approved BIT not null,
--longitude FLOAT not null,
--latitude FLOAT not null,
--google_api_placeID VARCHAR(900) not null,
--CONSTRAINT uc_API_Reference UNIQUE (google_api_placeID),
--CONSTRAINT pk_landmark_id PRIMARY KEY (id)
--);

CREATE TABLE app_user (
id INTEGER IDENTITY(1,1) not null,
username VARCHAR(64) not null,
user_password VARCHAR(64) not null,
salt VARCHAR(8000) not null,
is_admin BIT not null
CONSTRAINT pk_app_user_id PRIMARY KEY (id),
CONSTRAINT uc_username UNIQUE (username)

);

CREATE TABLE review (
id INTEGER IDENTITY(1,1) not null,
landmark_id INTEGER not null,
user_id INTEGER not null,
review_DATE DATE not null,
review_text VARCHAR(64) not null,
thumbs_up BIT not null DEFAULT 0,
thumbs_down BIT not null DEFAULT 0,
picture VARCHAR(64) null,

CONSTRAINT fk_review_landmark_id FOREIGN KEY (landmark_id) REFERENCES landmark(id),
CONSTRAINT fk_review_user_id FOREIGN KEY (user_id) REFERENCES app_user(id),
CONSTRAINT pk_review_id PRIMARY KEY (id)
);

CREATE TABLE itinerary (
id INTEGER IDENTITY(1,1) not null,
name VARCHAR(64) not null,
itinerary_DATE DATE not null,
user_id INTEGER not null,
starting_latitude FLOAT not null,
starting_longitude FLOAT not null,

CONSTRAINT pk_itinerary_id PRIMARY KEY (id),
CONSTRAINT fk_itinerary_user_id FOREIGN KEY (user_id) REFERENCES app_user(id)
);

CREATE TABLE itinerary_landmark (
landmark_id INTEGER not null,
itinerary_id INTEGER not null,

CONSTRAINT fk_itinerary_landmark_landmark_id FOREIGN KEY (landmark_id) REFERENCES landmark(id),
CONSTRAINT fk_itinerary_landmark_itinerary_id FOREIGN KEY (itinerary_id) REFERENCES itinerary(id),
CONSTRAINT pk_itinerary_landmark_landmark_id_itinerary_id PRIMARY KEY (landmark_id, itinerary_id)
);

INSERT INTO landmark ( image_name, landmark_description, name, admin_approved, longitude, latitude, google_api_placeID) VALUES ('ChristmasStoryHouse.jpg', 'A Christmas Story House is a museum in Cleveland, Ohio''s Tremont neighborhood. The 19th-century Victorian, which was used in the exterior scenes of Ralphie Parker''s house in the 1983 film A Christmas Story, was purchased by a private developer in 2004 and has been restored and renovated to appear as it did both inside and outside in the film. The museum is open to the public year round.', 'A Christmas Story House', 1, -81.687451, 41.468737,'ChIJbQVW7Kj6MIgRGb69WOQyj9M')
INSERT INTO landmark ( image_name, landmark_description, name, admin_approved, longitude, latitude, google_api_placeID) VALUES ('WestSideMarket.jpg', 'The West Side Market is the oldest operating indoor/outdoor market space in Cleveland, Ohio.  It is located at the corner of West 25th Street and Lorain Avenue in the Ohio City neighborhood. On December 18, 1973, it was added to the National Register of Historic Places.', 'West Side Market', 1, -81.702976, 41.485554,'ChIJnzfZu23wMIgRXsydHOM2tKs');
INSERT INTO landmark ( image_name, landmark_description, name, admin_approved, longitude, latitude, google_api_placeID) VALUES ('RockAndRollHallOfFame.jpg', 'The Rock and Roll Hall of Fame and Museum is a hall of fame and museum located on the shore of Lake Erie in downtown Cleveland, Ohio, United States. The Rock and Roll Hall of Fame Foundation was established on April 20, 1983, by Atlantic Records founder and chairman Ahmet Ertegun to recognize and archive the history of the best-known and most influential artists, producers, engineers, and other notable figures, who have each had some major influence on the development of rock and roll. In 1986, Cleveland was chosen as the hall of fame''s permanent home. Since opening in September 1995, the "Rock Hall" – part of the city''s redeveloped North Coast Harbor – has hosted more than 10 million visitors and had a cumulative economic impact estimated at more than $1.8 billion.', 'Rock and Roll Hall of Fame', 1, -81.69554, 41.509345,'ChIJHZLHDYPwMIgRXxZaKR6dG5c');
INSERT INTO landmark ( image_name, landmark_description, name, admin_approved, longitude, latitude, google_api_placeID) VALUES ('FranklinCastle.jpg', 'Franklin Castle (also known as the Hannes Tiedemann House) is a historical house located at 4308 Franklin Boulevard in Cleveland''s Ohio City neighborhood. The building has four stories and more than twenty rooms. It is purported to be the most haunted house in Ohio. On 15 March 1982, it was added to the National Register of Historic Places.', 'Franklin Castle', 1, -81.716523, 41.486504,'ChIJATNPEkHwMIgR_RUGQsdJ5Lw');
INSERT INTO landmark ( image_name, landmark_description, name, admin_approved, longitude, latitude, google_api_placeID) VALUES ('LeaguePark.jpg', 'League Park was a baseball park located in Cleveland, Ohio, United States. It is situated at the northeast corner of E. 66th Street and Lexington Avenue in the Hough neighborhood. It was built in 1891 as a wood structure and rebuilt using concrete and steel in 1910. The park was home to a number of professional sports teams, most notably the Cleveland Indians of Major League Baseball. League Park was first home to the Cleveland Spiders of the National League from 1891 to 1899 and of the Cleveland Lake Shores of the Western League, the minor league predecessor to the Indians, in 1900. In the late 1940s, the park was also the home field of the Cleveland Buckeyes of the Negro American League.', 'League Park', 1, -81.644167, 41.511389,'ChIJwTmNCbT7MIgRC-mBKTnsBmI');
INSERT INTO landmark ( image_name, landmark_description, name, admin_approved, longitude, latitude, google_api_placeID) VALUES ('GreatLakesBrewingCompany.png', 'Great Lakes Brewing Company is a brewery and brewpub in Cleveland, Ohio. The first brewpub and microbrewery in the state, Great Lakes Brewing has been named important both to Cleveland''s local IDENTITY, as well one of the initial forces behind the revival of the Ohio City neighborhood on the near West Side. In 2015, it was the 21st-largest craft brewery by volume and the 28th-largest overall brewery in the United States. The company was established in 1988 by brothers Patrick and Daniel Conway, both St. Edward High School graduates, in Cleveland''s Ohio City neighborhood, located near St. Ignatius High School and the West Side Market. The brewpub and restaurant remain in their original locations, while production has expanded to adjacent properties.', 'Great Lakes Brewing Company', 0, -81.704605, 41.485377,'ChIJA-V2u23wMIgRe6rtMBxi8uQ');
INSERT INTO landmark ( image_name, landmark_description, name, admin_approved, longitude, latitude, google_api_placeID) VALUES ('BorderlineCafe.jpg', 'Where all your breakfast dreams come true.', 'Borderline Cafe', 0, -81.827142, 41.482161, 'ChIJo41G2ojyMIgRnIlYYTTlbJIs'); 
INSERT INTO landmark ( image_name, landmark_description, name, admin_approved, longitude, latitude, google_api_placeID) VALUES ('Zoo.jpeg', 'Cleveland Metroparks Zoo has one of the largest collections of primates in North America, and features Monkey Island, a concrete island on which a large population of colobus monkeys are kept in free-range conditions (without cages or walls).', 'Cleveland Metroparks Zoo', 1, -81.7079, 41.4455, 'ChIJ24asjOvvMIgRI-vc_lsvA6M')
INSERT INTO landmark ( image_name, landmark_description, name, admin_approved, longitude, latitude, google_api_placeID) VALUES ('Aquarium.jpg', 'Occupying the historic FirstEnergy Powerhouse building located on the west bank of the Cuyahoga River in the citys Flats district, the aquarium which opened in January 2012 consists of approximately 70,000 square feet (6,500 m2) of exhibition space and features exhibits representing both local and exotic species of fish. The facility is the only free standing aquarium in the state of Ohio and ends a 26-year period that the city has been without a public aquarium.', 'Greater Cleveland Aquarium', 1, -81.7038, 41.4965, 'ChIJyRdfZGPwMIgRwsMVPtK-sSU')
INSERT INTO landmark ( image_name, landmark_description, name, admin_approved, longitude, latitude, google_api_placeID) VALUES ('Casino.jpg',  'The casino has 1,609 slot machines, 119 table games, and 35 electronic table games.','Jack Cleveland Casino', 1, -81.6931, 41.4986, 'ChIJyRdfZGPwMIgRwsMVPtK-sSU')
INSERT INTO landmark ( image_name, landmark_description, name, admin_approved, longitude, latitude, google_api_placeID) VALUES ('SeveranceHall.jpg', 'The hall has been the home of the Cleveland Orchestra since its opening on February 5, 1931. Severance Hall is listed on the National Register of Historic Places as part of the Wade Park District.', 'Severance Hall', 1, -81.6093, 41.5063, 'ChIJyRdfZGPwMIgRwsMVPtK-sSU')
INSERT INTO landmark ( image_name, landmark_description, name, admin_approved, longitude, latitude, google_api_placeID) VALUES ('USSCod.jpg', 'USS Cod (SS/AGSS/IXSS-224) is a Gato-class submarine, the only vessel of the United States Navy to be named for the cod, named after the worlds most important food fish of the North Atlantic and North Pacific.', 'USS Cod', 1, -81.6916, 41.5101, 'ChIJQ16XKoPwMIgRa5G5UJpGlR')
INSERT INTO landmark ( image_name, landmark_description, name, admin_approved, longitude, latitude, google_api_placeID) VALUES ('ArtMuseum.jpg', 'Founded in 1968 by Marjorie Talalay, Agnes Gund, and Nina Castelli Sundell as The New Gallery, the museum was renamed the Cleveland Center for Contemporary Art in 1984. In order to expand its exhibition space, in 1990 the museum moved to a 20,000-square-foot (1,900 m2) former Sears store on Carnegie Avenue that is now part of the Cleveland Play House complex which was renovated by Richard Fleischman + Partners Architects, Inc. to retrofit the space.', 'Museum of Contemporary Art Cleveland', 1, -81.6046, 41.5089, 'ChIJQ16XKoPwMIgRa5G5UJpGlRk')
INSERT INTO landmark ( image_name, landmark_description, name, admin_approved, longitude, latitude, google_api_placeID) VALUES ('GarfieldMemorial.jpg', 'The James A. Garfield Memorial was built in memory of the 20th U.S. President, James A. Garfield, who was assassinated in 1881. The country grieved for his loss almost as much as they had done for Lincoln, 16 years previously. In Washington, 100,000 plus citizens visited his casket, lying in state in the Capitol. Part of the memorial’s funding came from pennies sent in by children throughout the country.', 'Garfield Memorial', 1, -81.5914, 41.5101, 'ChIJQ16XKoPwMIgRa5G5UJpGlRk')
INSERT INTO landmark ( image_name, landmark_description, name, admin_approved, longitude, latitude, google_api_placeID) VALUES ('ClevelandArcade.jpg', 'Erected in 1890, at a cost of $867,000, the Arcade opened on Memorial Day (May 30, 1890),[and is identified as one of the earliest indoor shopping malls in the United States.', 'Cleveland Arcade', 1, -81.6906, 41.5004, 'ChIJQ16XKoPwMIgRa5G5UJpGlRk')
INSERT INTO landmark ( image_name, landmark_description, name, admin_approved, longitude, latitude, google_api_placeID) VALUES ('Botanical.jpg', 'Founded in 1930 as the Garden Center of Greater Cleveland. It was the first such organization in an American city. Originally housed in a converted boathouse on Wade Park Lagoon, the center served as a horticultural library, offering classes and workshops for gardeners and spearheading beautification projects in the community.', 'Cleveland Botanical Garden', 1, -81.6096, 41.5111, 'ChIJBScqa4z7MIgRNn276NOZPgs')
INSERT INTO landmark ( image_name, landmark_description, name, admin_approved, longitude, latitude, google_api_placeID) VALUES ('ChildrensMuseum.jpg', 'Dedicated to helping young children develop physically, emotionally, and socially. It also encourages adults to aid children in their development. One permanent exhibit, Splish! Splash!, is designed to teach children, through interactive displays, about water transportation.', 'Childrens Museum of Cleveland', 1, -81.6599, 41.5043, 'ChIJndgldIX7MIgRYSYwIAVP6WM')
INSERT INTO landmark ( image_name, landmark_description, name, admin_approved, longitude, latitude, google_api_placeID) VALUES ('WillardPark.jpg', 'It is the location of the public sculpture Free Stamp, and is the home of the original Cleveland Fire Fighters Memorial.', 'Willard Park', 1, -81.6925, 41.5055, 'ChIJWbAOGYLwMIgRKp8QKByB0HM')
INSERT INTO landmark ( image_name, landmark_description, name, admin_approved, longitude, latitude, google_api_placeID) VALUES ('Auto.jpg', 'As of 2011, the museum has 37 cars that are more than 100 years old, the largest such collection in the world.', 'Crawford Auto-Aviation Museum', 1, -81.6112, 41.5131, 'ChIJtQjHx4z7MIgRE2EaDg3BF3k')
INSERT INTO landmark ( image_name, landmark_description, name, admin_approved, longitude, latitude, google_api_placeID) VALUES ('Dittrick.jpg', 'The Dittrick Medical History Center is dedicated to the study of the history of medicine through a collection of rare books, museum artifacts, archives, and images.', 'Dittrick Museum of Medical History', 1, -81.6084, 41.5059, 'ChIJgcDo0Yj7MIgRKzs0r61cRYM')
INSERT INTO landmark ( image_name, landmark_description, name, admin_approved, longitude, latitude, google_api_placeID) VALUES ('EuclidBeach.jpg', 'Originally incorporated by investors from Cleveland and patterned after New Yorks Coney Island, the park was managed by William R. Ryan, Sr., who ran the park with featured attractions including vaudeville acts, concerts, gambling, a beer garden, and sideshows as well as a few early amusement rides.', 'Euclid Beach Park', 1, -81.570, 41.580, 'ChIJkTvkXw__MIgRItCFGaS_D0E')
INSERT INTO landmark ( image_name, landmark_description, name, admin_approved, longitude, latitude, google_api_placeID) VALUES ('WhiskeyIsland.jpg', 'The western half of Whiskey Island is occupied by the Cleveland Bulk Terminal and the eastern section is home to a marina and public park.', 'Whiskey Island', 1, -81.7165, 41.4959, 'ChIJ_RUQOFzwMIgRGZ8LSZ0iRno')
INSERT INTO landmark ( image_name, landmark_description, name, admin_approved, longitude, latitude, google_api_placeID) VALUES ('History.jpg', 'The museum was established in 1920 by Cyrus S. Eaton to perform research, education and development of collections in the fields of anthropology, archaeology, astronomy, botany, geology, paleontology, wildlife biology, and zoology.', 'Cleveland Museum of Natural History', 1, -81.6134, 41.5115, 'ChIJITY1VYz7MIgRH4nF3v7V9Tg')
INSERT INTO landmark ( image_name, landmark_description, name, admin_approved, longitude, latitude, google_api_placeID) VALUES ('LittleItaly.jpg', 'There are shops selling Italian goods as well as Italian restaurants lining the streets.', 'Little Italy', 1, -81.5981, 41.5089, 'ChIJleplAnT8MIgRjrY4Ql8O9eY')
INSERT INTO landmark ( image_name, landmark_description, name, admin_approved, longitude, latitude, google_api_placeID) VALUES ('East4th.jpg', 'The area is home to many famous Cleveland restaurants, bars, nightclubs, and apartments.', 'East 4th District', 1, -81.6901, 41.4990, 'ChIJleplAnT8MIgRjrY4Ql8O9eY')

INSERT INTO app_user (is_admin, username, user_password, salt) VALUES (0, 'visitor', 'sxWY9kuh1Jc1qhBnENn1n0VsoeI=','Dae9lZjJp90=');
INSERT INTO app_user (is_admin, username, user_password, salt) VALUES (1, 'admin', 'sxWY9kuh1Jc1qhBnENn1n0VsoeI=','Dae9lZjJp90=');

INSERT INTO itinerary (user_id, name, itinerary_DATE, starting_longitude, starting_latitude) VALUES (1, 'Itinerary 1', '20161201', -81.687451, 41.468737);
INSERT INTO itinerary (user_id, name, itinerary_DATE, starting_longitude, starting_latitude) VALUES (1, 'Cleveland Rocks Itinerary', '20161202', -81.687451, 41.468737);
INSERT INTO itinerary (user_id, name, itinerary_DATE, starting_longitude, starting_latitude) VALUES (1, 'Food Tour of Cleveland', '20161203', -81.687451, 41.468737);
INSERT INTO itinerary (user_id, name, itinerary_DATE, starting_longitude, starting_latitude) VALUES (1, 'That one time, in CLE', '20161204', -81.687451, 41.468737);
INSERT INTO itinerary (user_id, name, itinerary_DATE, starting_longitude, starting_latitude) VALUES (1, 'Itinerary 5', '20161205', -81.687451, 41.468737);

INSERT INTO itinerary_landmark (itinerary_id, landmark_id) VALUES (1, 2);
INSERT INTO itinerary_landmark (itinerary_id, landmark_id) VALUES (1, 3);
INSERT INTO itinerary_landmark (itinerary_id, landmark_id) VALUES (1, 4);
INSERT INTO itinerary_landmark (itinerary_id, landmark_id) VALUES (1, 5);
INSERT INTO itinerary_landmark (itinerary_id, landmark_id) VALUES (1, 6);
INSERT INTO itinerary_landmark (itinerary_id, landmark_id) VALUES (1, 7);

INSERT INTO review (landmark_id, user_id, review_DATE, review_text, thumbs_up, thumbs_down) VALUES (1, 1, '20161201', 'Christmas Story House? More like the lamp house!', 0, 1);
INSERT INTO review (landmark_id, user_id, review_DATE, review_text, thumbs_up, thumbs_down) VALUES (1, 2, '20161202', '''I love lamp '' - Brick Tamland, City Administrator', 1, 0);
