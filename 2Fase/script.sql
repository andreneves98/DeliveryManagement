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
	select nome, nif, nr_tel, marcaveiculo, matricula from ServEntr.Motorista;
GO

-- RETORNA TODOS OS CLIENTES --
create function ServEntr.ListClientes() returns table
as
	return
	select nome, nif, nr_tel, email, morada from ServEntr.Cliente;
GO
