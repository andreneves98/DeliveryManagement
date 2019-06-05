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

-- RTORNA TODOS OS PRODUTOS --
create function ServEntr.ListProdutos(@estabelecimento varchar(255)) returns table
as
	return
	select * from ServEntr.Produto
	where nome_estab = @estabelecimento;
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

-- RETORNA TODOS OS ESTABELECIMENTOS --
create function ServEntr.ListEstabelecimentos() returns table
as
	return
	select nome from ServEntr.Estabelecimento;
GO

select * from ServEntr.ListEstabelecimentos();

-- INSERE NOVO CLIENTE --
create proc ServEntr.NewCliente (@nome varchar(255), @nif int, @nr_tel int, @email varchar(255), @morada varchar(255))
as
	begin
	declare @id as int;
	select @id = max(nr_reg)+1 from ServEntr.Cliente;
	insert into ServEntr.Cliente(nr_reg, nome, nif, nr_tel, email, morada) values (@id, @nome, @nif, @nr_tel, @email, @morada);

	end
GO

-- INSERE NOVO MOTORISTA --
create proc ServEntr.NewMotorista(@nome varchar(255), @nif int, @nr_tel int, @email varchar(255), @morada varchar(255), @matricula varchar(15), @marcaveiculo varchar(30))
as
begin
	declare @id as int;
	select @id = max(nr_reg)+1 from ServEntr.Motorista;
	insert into ServEntr.Motorista(nr_reg, nome, nif, nr_tel, email, morada, matricula, marcaveiculo) values (@id, @nome, @nif, @nr_tel, @email, @morada, @matricula, @marcaveiculo);
end


-- EDITA CLIENTE --
create proc ServEntr.editCliente (@nome varchar(255), @nif int, @nr_tel int, @email varchar(255), @morada varchar(255))
as
	begin
	update ServEntr.Cliente set nif = @nif, nr_tel = @nr_tel, email = @email, morada = @morada
	where nome = @nome;

	end
GO


-- ELIMINA CLIENTE --
create proc ServEntr.deleteCliente (@nome varchar(255), @nif int, @nr_tel int, @email varchar(255), @morada varchar(255))
as
	begin
	delete from ServEntr.Cliente where nome=@nome;
	end
GO

-- ELIMINA MOTORISTA --
create proc ServEntr.deleteMotorista (@nome varchar(255), @nif int, @nr_tel int, @email varchar(255), @morada varchar(255))
as
	begin
	delete from ServEntr.Motorista where nome=@nome;
	end
GO

-- EDITA MOTORISTA --
create proc ServEntr.editMotorista (@nome varchar(255), @nif int, @nr_tel int, @email varchar(255), @morada varchar(255), @matricula varchar(15), @marcaveiculo varchar(15))
as
	begin
	update ServEntr.Motorista set nif = @nif, nr_tel = @nr_tel, email = @email, morada = @morada, matricula = @matricula, marcaveiculo = @marcaveiculo
	where nome = @nome;

	end
GO


-- INSERE NOVA ENCOMENDA --
create proc ServEntr.newEncomenda (@estabelecimento varchar(255), @cliente varchar(255), @produtos varchar(255), @obs varchar(255), @metodo varchar(30), @preco_total int OUTPUT)
as
begin
	declare @prod as varchar(100);
	select @prod from STRING_SPLIT(@produtos, '-');

	select preco from ServEntr.Produto where nome=@prod;

	set @preco_total = sum(preco);

	declare @ref_p as int;
	set @ref_p = rand();

	declare @id as varchar(5);
	set @id = rand();

	select morada as morada_cl from ServEntr.Cliente where nome = @nome;
	select nr_reg as nr_reg_cliente from ServEntr.Cliente where nome = @nome;
	
	select count(nr_reg) as nr_reg_motorista from ServEntr.Motorista;
	declare @moto as int;
	set @moto = rand(nr_reg_motorista);
	

	insert into ServEntr.Encomenda(id, morada, obs, preco_total, ref_pagamento, nr_registo_cliente, nr_reg_motorista, id_avaliacao) values (@id, @moradacl, @preco_total, @obs, @nr_reg_cliente, @moto, null);
	
	declare @ref as int;
	set @ref = floor(rand()*999999); 

	insert into ServEntr.Pagamento(preco, morada_fat, metodo, referencia) values(@preco_total, @morada_cl, @metodo, @ref);

end





