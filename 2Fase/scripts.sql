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

-- RETORNA TODAS AS MARCAS DE VEÍCULOS --
create function ServEntr.ListMarcas() returns table
as
	return
	select distinct marcaveiculo from ServEntr.Motorista;
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

-- VERIFICA NOVO CLIENTE --
create trigger ServEntr.t_newCliente on ServEntr.Cliente
after insert
as
begin
	
	declare @nome as varchar(255);

	select @nome = nome from ServEntr.Cliente;

	if exists(select nome from ServEntr.Cliente where nome = @nome)
	begin
		rollback tran;
		raiserror('Cliente já existe na Base de dado!', 16, 1);
	end


end

-- INSERE NOVO MOTORISTA --
create proc ServEntr.NewMotorista(@nome varchar(255), @nif int, @nr_tel int, @email varchar(255), @morada varchar(255), @matricula varchar(15), @marcaveiculo varchar(30))
as
begin
	declare @id as int;
	select @id = max(nr_reg)+1 from ServEntr.Motorista;
	insert into ServEntr.Motorista(nr_reg, nome, nif, nr_tel, email, morada, matricula, marcaveiculo) values (@id, @nome, @nif, @nr_tel, @email, @morada, @matricula, @marcaveiculo);
end

-- VERIFICA NOVO MOTORISTA --
create trigger ServEntr.t_newMotorista on ServEntr.Motorista
after insert
as
begin
	
	declare @nome as varchar(255);

	select @nome = nome from ServEntr.Motorista;

	if exists(select nome from ServEntr.Motorista where nome = @nome)
	begin
		rollback tran;
		raiserror('Motorista já existe na Base de dados!', 16, 1);
	end
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
create proc ServEntr.deleteCliente (@nome varchar(255))
as
	begin
	delete from ServEntr.Cliente where nome=@nome;
	end
GO

-- ELIMINA MOTORISTA --
create proc ServEntr.deleteMotorista (@nome varchar(255))
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

-- EDITA ENCOMENDA --
create proc ServEntr.editEncomenda (@id varchar(5))
as
	begin
	update ServEntr.Encomenda set status = 'terminado'
	where id = @id;
	end
GO

-- INSERE NOVA ENCOMENDA --
create proc ServEntr.newEncomenda (@estabelecimento varchar(255), @cliente varchar(255), @produtos varchar(255), @obs varchar(255), @metodo varchar(30))
as
begin
	--declare @preco_total as int;
	--declare @prod as varchar(100);
	--select @prod from STRING_SPLIT(@produtos, '-');

	--declare @preco as decimal(6,2);
	--select @preco from ServEntr.Produto where nome=@prod;

	--set @preco_total = sum(@preco);

	--exec ServEntr.newEncomendaPrice @produtos, @preco_total OUTPUT;
	--select @preco_total;

	declare @ref as int;
	set @ref = 5484; 

	declare @id as varchar(5);
	set @id = floor(rand()*99999);

	declare @morada_cl as varchar(255);
	select @morada_cl = morada from ServEntr.Cliente where nome = @cliente;
	declare @nr_reg_cliente as int;
	select @nr_reg_cliente = nr_reg from ServEntr.Cliente where nome = @cliente;
	
	declare @nr_reg_motorista as int;
	select @nr_reg_motorista = count(nr_reg) from ServEntr.Motorista;
	declare @moto as int;
	set @moto = floor(rand()*@nr_reg_motorista);

	declare @id_aval as int;
	set @id_aval = floor(rand()*999999)+0;

	insert into ServEntr.Encomenda(id, morada, obs, preco_total, ref_pagamento, nr_reg_cliente, nr_reg_motorista, id_avaliacao, status) values (@id, @morada_cl, @obs, 0, @ref, @nr_reg_cliente, @moto, @id_aval, 'ativa');
	--insert into ServEntr.Pagamento(preco, morada_fat, metodo, referencia) values(0, @morada_cl, @metodo, @ref);
	--insert into ServEntr.Avaliacao(id, obs, estrelas_compra, estrelas_motorista, tempo_espera) values (@id_aval, null, null, null, null);

end



-- RETORNA UM CLIENTE PELO NOME--
create function ServEntr.List1Cliente(@nome as varchar(255)) returns table
as
	return
	select nome, nif, nr_tel, email, morada from ServEntr.Cliente
	where nome = @nome;
GO

-- RETORNA UM CLIENTE PELO NR_REG--
create function ServEntr.List1ClienteNr(@nr_reg as int) returns table
as
	return
	select nome, nif, nr_tel, email, morada from ServEntr.Cliente
	where nr_reg = @nr_reg;
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
	select ServEntr.Cliente.nome, id, ServEntr.Encomenda.morada, obs, preco_total, ref_pagamento, nr_reg_cliente, nr_reg_motorista, id_avaliacao, status from ServEntr.Encomenda
	join ServEntr.Cliente on nr_reg_cliente = ServEntr.Cliente.nr_reg
	where id = @id;
GO

-- PREÇO TOTAL ENCOMENDA --
create proc ServEntr.newEncomendaPrice (@produtos varchar(255), @preco_total int OUTPUT)
as
begin
	declare @prod as varchar(100);
	select @prod from STRING_SPLIT(@produtos, '-');

	declare @preco as decimal(6,2);
	select @preco from ServEntr.Produto where nome=@prod;

	set @preco_total = sum(@preco);
end

create proc ServEntr.updateAvaliacao (@id int, @obs varchar(255), @estrelas_compra int, @estrelas_motorista int, @tempo_espera int)
as
begin
	
	update ServEntr.Avaliacao set obs = @obs, estrelas_compra = @estrelas_compra, estrelas_motorista = @estrelas_motorista, tempo_espera = @tempo_espera
	where id = @id;
	
end




