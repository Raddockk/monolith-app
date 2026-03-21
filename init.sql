create table role(
    id smallserial primary key unique,
    name varchar(10) not null
);

create table family (
    id bigserial primary key,
    name varchar(30) not null
);

create table category (
    id smallserial primary key,
    name varchar(50) not null
);

create table bank (
    id smallserial primary key,
    name varchar(30) not null
);

create table "user" (
    id bigserial primary key,
    login varchar(32) unique not null,
    mail varchar(50) unique,
    password_hash varchar(32) not null,   
    firstname varchar(50) not null,
    lastname varchar(50) not null,
    patronymic varchar(50),
    age int2,
    role_id int2 not null,
    foreign key (role_id) references role(id)
);

create table family_member ( 
	id bigserial primary key,
    user_id int8 not null,
    family_id int8 not null,
    join_date date default current_date,
    foreign key (user_id) references "user"(id),
    foreign key (family_id) references family(id)    
);

create table goal(
    id smallserial primary key,
    name varchar(50) not null,
    target_amount decimal(12,2) not null,
    current_amount decimal(12,2) not null,
    family_id int8 not null,
    created_by_id int8 not null,
    foreign key (family_id) references family(id),
    foreign key (created_by_id) references "user"(id)
);

create table account (
    id smallserial primary key,
    name varchar(50) not null,
    balance decimal(12,2) not null,
    currency varchar(3) not null,
    bank_id int2,
    user_id int8 not null,    
    foreign key (bank_id) references bank(id),
    foreign key (user_id) references "user"(id)    
);

create table transaction (
    id serial primary key,
    amount decimal(12,2) not null,
    type varchar(7) not null,
    transaction_date timestamp not null,
    account_id int2 not null,
    category_id int2,
    user_id int8 not null,
    foreign key (user_id) references "user"(id),
    foreign key (account_id) references account(id),
    foreign key (category_id) references category(id)
);

create table budget(
    id smallserial primary key,
    budget_amount decimal(12,2) not null,
    period varchar(10) not null,
    start_date date not null,
    end_date date,
    family_id int8 not null,
    category_id int2,
    created_by_id int8 not null,
    foreign key (family_id) references family(id),
    foreign key (category_id) references category(id),
    foreign key (created_by_id) references "user"(id)
);