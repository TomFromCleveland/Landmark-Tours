
drop table itinerary_landmark;
drop table itinerary;
drop table review;
drop table app_user;
drop table landmark;

create table landmark (
id integer identity(1,1) not null,
image_name varchar(200) not null,
landmark_description varchar(8000) not null,
name varchar(64) not null,
admin_approved bit not null,
longitude float not null,
latitude float not null,
google_api_reference varchar(8000) not null,
constraint uc_API_Reference UNIQUE (google_api_reference),
constraint pk_landmark_id primary key (id)
);

create table app_user (
id integer identity(1,1) not null,
user_type varchar(64) not null,
username varchar(64) not null,
user_password varchar(64) not null,

constraint pk_app_user_id primary key (id)
);

create table review (
id integer identity(1,1) not null,
landmark_id integer not null,
user_id integer not null,
review_date date not null,
review_text varchar(64) not null,
thumbs_up bit not null default 0,
thumbs_down bit not null default 0,
picture varchar(64) null,

constraint fk_review_landmark_id foreign key (landmark_id) references landmark(id),
constraint fk_review_user_id foreign key (user_id) references app_user(id),
constraint pk_review_id primary key (id)
);

create table itinerary (
id integer identity(1,1) not null,
user_id integer not null,
--starting point goes here

constraint pk_itinerary_id primary key (id),
constraint fk_itinerary_user_id foreign key (user_id) references app_user(id)
);

create table itinerary_landmark (
landmark_id integer not null,
itinerary_id integer not null,

constraint fk_itinerary_landmark_landmark_id foreign key (landmark_id) references landmark(id),
constraint fk_itinerary_landmark_itinerary_id foreign key (itinerary_id) references itinerary(id),
constraint pk_itinerary_landmark_landmark_id_itinerary_id primary key (landmark_id, itinerary_id)
);

INSERT INTO landmark ( image_name, landmark_description, name, admin_approved, longitude, latitude) VALUES ('ChristmasStoryHouse.jpg', 'A Christmas Story House is a museum in Cleveland, Ohio''s Tremont neighborhood. The 19th-century Victorian, which was used in the exterior scenes of Ralphie Parker''s house in the 1983 film A Christmas Story, was purchased by a private developer in 2004 and has been restored and renovated to appear as it did both inside and outside in the film. The museum is open to the public year round.', 'A Christmas Story House', 1, -81.687451, 41.468737)
INSERT INTO landmark ( image_name, landmark_description, name, admin_approved, longitude, latitude) VALUES ('WestSideMarket.jpg', 'The West Side Market is the oldest operating indoor/outdoor market space in Cleveland, Ohio.  It is located at the corner of West 25th Street and Lorain Avenue in the Ohio City neighborhood. On December 18, 1973, it was added to the National Register of Historic Places.', 'West Side Market', 1, -81.702976, 41.485554);
INSERT INTO landmark ( image_name, landmark_description, name, admin_approved, longitude, latitude) VALUES ('RockAndRollHallOfFame.jpg', 'The Rock and Roll Hall of Fame and Museum is a hall of fame and museum located on the shore of Lake Erie in downtown Cleveland, Ohio, United States. The Rock and Roll Hall of Fame Foundation was established on April 20, 1983, by Atlantic Records founder and chairman Ahmet Ertegun to recognize and archive the history of the best-known and most influential artists, producers, engineers, and other notable figures, who have each had some major influence on the development of rock and roll. In 1986, Cleveland was chosen as the hall of fame''s permanent home. Since opening in September 1995, the "Rock Hall" – part of the city''s redeveloped North Coast Harbor – has hosted more than 10 million visitors and had a cumulative economic impact estimated at more than $1.8 billion.', 'Rock and Roll Hall of Fame', 1, -81.69554, 41.509345);
INSERT INTO landmark ( image_name, landmark_description, name, admin_approved, longitude, latitude) VALUES ('FranklinCastle.jpg', 'Franklin Castle (also known as the Hannes Tiedemann House) is a historical house located at 4308 Franklin Boulevard in Cleveland''s Ohio City neighborhood. The building has four stories and more than twenty rooms. It is purported to be the most haunted house in Ohio. On 15 March 1982, it was added to the National Register of Historic Places.', 'Franklin Castle', 1, -81.716523, 41.486504);
INSERT INTO landmark ( image_name, landmark_description, name, admin_approved, longitude, latitude) VALUES ('LeaguePark.jpg', 'League Park was a baseball park located in Cleveland, Ohio, United States. It is situated at the northeast corner of E. 66th Street and Lexington Avenue in the Hough neighborhood. It was built in 1891 as a wood structure and rebuilt using concrete and steel in 1910. The park was home to a number of professional sports teams, most notably the Cleveland Indians of Major League Baseball. League Park was first home to the Cleveland Spiders of the National League from 1891 to 1899 and of the Cleveland Lake Shores of the Western League, the minor league predecessor to the Indians, in 1900. In the late 1940s, the park was also the home field of the Cleveland Buckeyes of the Negro American League.', 'League Park', 1, -81.644167, 41.511389);
INSERT INTO landmark ( image_name, landmark_description, name, admin_approved, longitude, latitude) VALUES ('GreatLakesBrewingCompany.png', 'Great Lakes Brewing Company is a brewery and brewpub in Cleveland, Ohio. The first brewpub and microbrewery in the state, Great Lakes Brewing has been named important both to Cleveland''s local identity, as well one of the initial forces behind the revival of the Ohio City neighborhood on the near West Side. In 2015, it was the 21st-largest craft brewery by volume and the 28th-largest overall brewery in the United States. The company was established in 1988 by brothers Patrick and Daniel Conway, both St. Edward High School graduates, in Cleveland''s Ohio City neighborhood, located near St. Ignatius High School and the West Side Market. The brewpub and restaurant remain in their original locations, while production has expanded to adjacent properties.', 'Great Lakes Brewing Company', 0, -81.704605, 41.485377);
INSERT INTO landmark ( image_name, landmark_description, name, admin_approved, longitude, latitude) VALUES ('BorderlineCafe.jpg', 'Where all your breakfast dreams come true.', 'Borderline Cafe', 0, -81.827142, 41.482161); 
