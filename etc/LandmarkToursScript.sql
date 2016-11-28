
create table landmark (
id integer not null,
--location goes here
landmark_description varchar(64) not null,
name varchar(64) not null,
review_pending bit not null,

constraint pk_landmark_id primary key (id)
);

create table app_user (
id integer not null,
user_type varchar(64) not null, 
username varchar(64) not null,
user_password varchar(64) not null,

constraint pk_app_user_id primary key (id)
);

create table review (
landmark_id integer not null,
user_id integer not null,
review_date date not null,
review_text varchar(64) not null,
thumbs_up bit not null default 0,
thumbs_down bit not null default 0,
picture varchar(64) null,

constraint fk_review_landmark_id foreign key (landmark_id) references landmark(id),
constraint fk_review_user_id foreign key (user_id) references app_user(id)
);

create table itinerary (
id integer not null,
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