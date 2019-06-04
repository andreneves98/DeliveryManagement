--create schema ServEntr;
create table ServEntr.Cliente(
	nr_reg		int,
	nome		varchar(255) not null,
	nif			int not null unique,
	email		varchar(255) not null unique,
	nr_tel		int not null unique,
	morada		varchar(255),
	primary key(nr_reg));

create table ServEntr.Motorista(
	nr_reg		int,
	nome		varchar(255) not null,
	nif			int not null unique,
	email		varchar(255) not null unique,
	nr_tel		int not null unique,
	morada		varchar(255),
	matricula		varchar(15) not null unique,
	marcaveiculo	varchar(30) not null,
	primary key(nr_reg));

create table ServEntr.Estabelecimento(
	nome			varchar(255),
	tipo			varchar(30) not null,
	morada			varchar(255) not null unique,
	horario			varchar(1000) not null,
	primary key(nome));

create table ServEntr.Produto(
	id			varchar(5),
	nome		varchar(100) not null unique,
	preco		decimal(6,2) not null,
	descricao	varchar(255) not null,
	nome_estab  varchar(255),
	primary key(id),
	foreign key(nome_estab) references ServEntr.Estabelecimento(nome)
				on delete cascade,	-- ao apagar o estabelecimento, apaga os produtos dele
	check(preco > 0));

create table ServEntr.Avaliacao(
	id					int unique,
	obs					varchar(255),
	estrelas_compra		int not null,
	estrelas_motorista	int not null,
	tempo_espera		int,
	primary key(id));

create table ServEntr.Pagamento(
	preco			decimal(6,2),
	morada_fat		varchar(255) not null unique,
	metodo			varchar(30) not null,
	referencia		int not null unique,
	primary key(referencia));

create table ServEntr.Encomenda(
	id				varchar(5),
	morada			varchar(255) not null unique,
	obs				varchar(255),
	preco_total		decimal(6,2),
	ref_pagamento	int,
	nr_reg_cliente	int not null unique,
	nr_reg_motorista int not null unique,
	id_avaliacao	int,
	primary key(id),
	foreign key(ref_pagamento) references ServEntr.Pagamento(referencia),
	foreign key(nr_reg_cliente) references ServEntr.Cliente(nr_reg)
				on delete cascade,	-- ao apagar o cliente, apaga as encomendas dele
	foreign key(nr_reg_motorista) references ServEntr.Motorista(nr_reg)
				on delete cascade,	-- ao apagar o motorista, apaga as encomendas dele
	foreign key(id_avaliacao) references ServEntr.Avaliacao(id));

alter table ServEntr.Encomenda add status varchar(15);

select * from ServEntr.Cliente;
select * from ServEntr.Motorista;
select * from ServEntr.Estabelecimento;
select * from ServEntr.Produto;
select * from ServEntr.Avaliacao;
select * from ServEntr.Pagamento;
select * from ServEntr.Encomenda;
