-- RETURNA O NÚMERO TOTAL DE ENCOMENDAS --
CREATE FUNCTION ServEntr.Num_Encomendas() RETURNS INT
AS
BEGIN
	DECLARE @total INT;
	select @total = count(*) FROM ServEntr.Encomenda;
	RETURN @total;
END;
GO

-- RETORNA O NÚMERO DE ENCOMENDAS ATIVAS --
CREATE function ServEntr.Num_Encomendas_Ativas() returns int
as
begin
	declare @ativas int;
	select @ativas = count(*) from ServEntr.Encomenda
	where status = 'ativa';
	return @ativas;
end;
GO

-- RETORNA O NÚMERO DE ENCOMENDAS TERMINADAS --
CREATE function ServEntr.Num_Encomendas_Terminadas() returns int
as
begin
	declare @terminadas int;
	select @terminadas = count(*) from ServEntr.Encomenda
	where status = 'terminada';
	return @terminadas;
end;
GO

-- RETORNA O NÚMERO DE ENCOMENDAS TERMINADAS --
CREATE function ServEntr.Num_Encomendas_Canceladas() returns int
as
begin
	declare @canceladas int;
	select @canceladas = count(*) from ServEntr.Encomenda
	where status = 'cancelada';
	return @canceladas;
end;
GO

-- RETORNA TODAS AS ENCOMENDAS --
create function ServEntr.ListEncomendas() returns table
as
	return
	select id, ServEntr.Cliente.nome, ServEntr.Encomenda.morada, preco_total, status 
	from ServEntr.Encomenda 
	join ServEntr.Cliente on nr_reg = nr_reg_cliente;
GO

-- RETORNA TODOS OS MOTORISTAS --
create function ServEntr.ListMotoristas() returns table
as
	return
	select nr_reg, nome, nif, nr_tel, marcaveiculo, matricula from ServEntr.Motorista;
GO

-- RETORNA TODOS OS CLIENTES --
create function ServEntr.ListClientes() returns table
as
	return
	select nr_reg, nome, nif, nr_tel, email, morada from ServEntr.Cliente;
GO

-- RETORNA TODOS OS ESTABELECIMENTOS --
create function ServEntr.ListEstabelecimentos() returns table
as
	return
	select nome from ServEntr.Estabelecimento;
GO

-- RETORNA UM CLIENTE --
create function ServEntr.List1Cliente(@nome as varchar(255)) returns table
as
	return
	select nome, nif, nr_tel, email, morada from ServEntr.Cliente
	where nome = @nome;
GO

-- RETORNA UM Motorista --
create function ServEntr.List1Motorista(@nome as varchar(255)) returns table
as
	return
	select nome, nif, nr_tel, email, morada, matricula, marcaveiculo from ServEntr.Motorista
	where nome = @nome;
GO

-- RETORNA UMA ENCOMENDA --
create function ServEntr.List1Encomenda(@id as varchar(5)) returns table
as
	return
	select ServEntr.Cliente.nome as cliente, id, ServEntr.Encomenda.morada, preco_total, status, ServEntr.Pagamento.metodo, obs, produtos.nome as produtos
	from ServEntr.Encomenda
	join ServEntr.Cliente on ServEntr.Cliente.morada = ServEntr.Encomenda.morada
	join ServEntr.Pagamento on ServEntr.Pagamento.referencia = ServEntr.Encomenda.ref_pagamento
	cross apply ServEntr.ProdutosEncomenda(@id) produtos
	where id = @id;
GO

-- RETORNA UM ESTABELECIMENTO --
create function ServEntr.List1Estabelecimento(@nome as varchar(255)) returns table
as
	return
	select * from ServEntr.Estabelecimento
	where nome = @nome;
GO

-- RETORNA UMA AVALIAÇÃO --
create function ServEntr.List1Avaliacao() returns table
as
	return
	select id from ServEntr.Avaliacao;
GO

create function ServEntr.ListMarcas() returns table
as
	return
	select distinct marcaveiculo from ServEntr.Motorista;
GO

-- RETORNA OS PRODUTOS DE UM ESTABELECIMENTO --
create function ServEntr.ListProdutos(@estabelecimento varchar(255)) returns table
as
	return
	select * from ServEntr.Produto
	where nome_estab = @estabelecimento;
GO

-- RETORNA OS PRODUTOS DE UMA ENCOMENDA --
create function ServEntr.ProdutosEncomenda(@id_encomenda varchar(5)) returns table
as
	return
	select ServEntr.EncomendaProduto.id_produto, ServEntr.Produto.nome 
	from ServEntr.EncomendaProduto
	join ServEntr.Produto on ServEntr.EncomendaProduto.id_produto = ServEntr.Produto.id
	where ServEntr.EncomendaProduto.id_encomenda = @id_encomenda;
GO