
DROP TABLE itinerary_landmark;
DROP TABLE itinerary;
DROP TABLE review;
DROP TABLE app_user;
DROP TABLE landmark;

CREATE TABLE landmark (
id INTEGER IDENTITY(1,1) not null,
image_name VARCHAR(200) not null,
landmark_description VARCHAR(8000) not null,
name VARCHAR(64) not null,
admin_approved BIT not null,
longitude FLOAT not null,
latitude FLOAT not null,
google_api_placeID VARCHAR(900) not null,
CONSTRAINT uc_API_Reference UNIQUE (google_api_placeID),
CONSTRAINT pk_landmark_id PRIMARY KEY (id)
);

CREATE TABLE app_user (
id INTEGER IDENTITY(1,1) not null,
user_type VARCHAR(64) not null,
username VARCHAR(64) not null,
user_password VARCHAR(64) not null,

CONSTRAINT pk_app_user_id PRIMARY KEY (id)
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
starting_latitude INTEGER not null,
starting_longitude INTEGER not null

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
INSERT INTO landmark ( image_name, landmark_description, name, admin_approved, longitude, latitude, google_api_placeID) VALUES ('BorderlineCafe.jpg', 'Where all your breakfast dreams come true.', 'Borderline Cafe', 0, -81.827142, 41.482161,'ChIJo41G2ojyMIgRnIlYYTTlbJIs'); 

INSERT INTO app_user (user_type, username, user_password) VALUES ('city visitor', 'visitor', 'password');
INSERT INTO app_user (user_type, username, user_password) VALUES ('city administrator', 'admin', 'password');

INSERT INTO itinerary (user_id, name, itinerary_DATE, starting_latitude, starting_longitude) VALUES (1, 'Itinerary 1', '20161201', -81.687451, 41.468737);
INSERT INTO itinerary (user_id, name, itinerary_DATE, starting_latitude, starting_longitude) VALUES (1, 'Cleveland Rocks Itinerary', '20161202', -81.687451, 41.468737);
INSERT INTO itinerary (user_id, name, itinerary_DATE, starting_latitude, starting_longitude) VALUES (1, 'Food Tour of Cleveland', '20161203', -81.687451, 41.468737);
INSERT INTO itinerary (user_id, name, itinerary_DATE, starting_latitude, starting_longitude) VALUES (1, 'That one time, in CLE', '20161204', -81.687451, 41.468737);
INSERT INTO itinerary (user_id, name, itinerary_DATE, starting_latitude, starting_longitude) VALUES (1, 'Itinerary 5', '20161205', -81.687451, 41.468737);

INSERT INTO itinerary_landmark (itinerary_id, landmark_id) VALUES (1, 2);
INSERT INTO itinerary_landmark (itinerary_id, landmark_id) VALUES (1, 3);
INSERT INTO itinerary_landmark (itinerary_id, landmark_id) VALUES (1, 4);
INSERT INTO itinerary_landmark (itinerary_id, landmark_id) VALUES (1, 5);
INSERT INTO itinerary_landmark (itinerary_id, landmark_id) VALUES (1, 6);
INSERT INTO itinerary_landmark (itinerary_id, landmark_id) VALUES (1, 7);

INSERT INTO review (landmark_id, user_id, review_DATE, review_text, thumbs_up, thumbs_down) VALUES (1, 1, '20161201', 'Christmas Story House? More like the lamp house!', 0, 1);
INSERT INTO review (landmark_id, user_id, review_DATE, review_text, thumbs_up, thumbs_down) VALUES (1, 2, '20161202', '''I love lamp '' - Brick Tamland, City Administrator', 1, 0);