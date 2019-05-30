--create schema ServEntr;

create table ServEntr.Utilizador(
	nr_reg		int,
	nome		varchar(255) not null,
	nif			int not null unique,
	email		varchar(255) not null unique,
	nr_tel		int not null unique,
	primary key(nr_reg));

create table ServEntr.Produto(
	id			varchar(5),
	nome		varchar(100) not null unique,
	preco		decimal(6,2) not null,
	descricao	varchar(255) not null,
	primary key(id),
	check(preco > 0));

create table ServEntr.Estabelecimento(
	nome			varchar(255),
	tipo			varchar(30) not null,
	morada			varchar(255) not null unique,
	horario			varchar(1000) not null,
	id_avaliacao	varchar(5) unique,
	id_produto		varchar(5) unique,
	primary key(nome),
	foreign key(id_produto) references ServEntr.Produto(id)
				on delete set null on update cascade);

create table ServEntr.Cliente(
	promo_code	varchar(10) unique,
	nr_reg		int,
	primary key(nr_reg),
	foreign key(nr_reg) references ServEntr.Utilizador(nr_reg));

create table ServEntr.Motorista(
	matricula		varchar(6) not null unique,
	marcaveiculo	varchar(15) not null,
	nr_reg			int,
	id_avaliacao	varchar(5) unique,
	primary key(nr_reg),
	foreign key(nr_reg) references ServEntr.Utilizador(nr_reg));

create table ServEntr.Encomenda(
	id				varchar(5),
	morada			varchar(255) not null unique,
	obs				varchar(255),
	preco_total		decimal(6,2),
	id_produto		varchar(5) unique,
	ref_pagamento	int not null unique,
	nr_reg_cliente	int not null unique,
	nr_reg_motorista int not null unique,
	primary key(id),
	foreign key(id_produto) references ServEntr.Produto(id)
				on delete set null on update cascade,
	foreign key(nr_reg_cliente) references ServEntr.Cliente(nr_reg) 
				on update cascade,
	foreign key(nr_reg_motorista) references ServEntr.Motorista(nr_reg)
				on update cascade);

create table ServEntr.Pagamento(
	preco			decimal(6,2),
	morada_fat		varchar(255) not null unique,
	metodo			varchar(30) not null,
	conta_banc		bigint not null unique,
	referencia		int,
	id_encomenda	varchar(5) unique,
	primary key(referencia),
	foreign key(id_encomenda) references ServEntr.Encomenda(id)
				on delete set null on update cascade);

alter table ServEntr.Encomenda
add foreign key(ref_pagamento) references ServEntr.Pagamento(referencia);

create table ServEntr.Avaliacao(
	id					int unique,
	obs					varchar(255),
	estrelas_compra		int not null,
	estrelas_motorista	int not null,
	tempo_espera		int,
	id_encomenda		varchar(5) unique,
	primary key(id),
	foreign key(id_encomenda) references ServEntr.Encomenda(id)
				on delete set null on update cascade);

alter table ServEntr.Encomenda add status varchar(10);

select * from ServEntr.Encomenda;
alter table ServEntr.Utilizador add morada varchar(255) not null;
alter table ServEntr.Utilizador add cod_postal varchar(8) not null;