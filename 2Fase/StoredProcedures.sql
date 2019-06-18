-- INSERE NOVO CLIENTE --
create proc ServEntr.NewCliente (@nome varchar(255), @nif int, @nr_tel int, @email varchar(255), @morada varchar(255))
as
	begin
	declare @id as int;
	select @id = max(nr_reg)+1 from ServEntr.Cliente;
	insert into ServEntr.Cliente(nr_reg, nome, nif, nr_tel, email, morada) values (@id, @nome, @nif, @nr_tel, @email, @morada);

	end
GO

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

-- INSERE NOVO MOTORISTA --
create proc ServEntr.NewMotorista(@nome varchar(255), @nif int, @nr_tel int, @email varchar(255), @morada varchar(255), @matricula varchar(15), @marcaveiculo varchar(30))
as
begin
	declare @id as int;
	select @id = max(nr_reg)+1 from ServEntr.Motorista;
	insert into ServEntr.Motorista(nr_reg, nome, nif, nr_tel, email, morada, matricula, marcaveiculo) values (@id, @nome, @nif, @nr_tel, @email, @morada, @matricula, @marcaveiculo);
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

-- ELIMINA MOTORISTA --
create proc ServEntr.deleteMotorista (@nome varchar(255))
as
	begin
	delete from ServEntr.Motorista where nome=@nome;
	end
GO

-- INSERE NOVA ENCOMENDA --
create proc ServEntr.newEncomenda (@estabelecimento varchar(255), @cliente varchar(255), @produtos varchar(255), @obs varchar(255), @metodo varchar(30))
as
begin
	
	declare @preco_total as int;
	exec ServEntr.newEncomendaPrice @produtos, @preco_total OUTPUT;
	select @preco_total;

	declare @ref as int;
	set @ref = floor(rand()*999999); 

	declare @id as varchar(5);
	set @id = floor(rand()*99999);

	declare @morada_cl as varchar(255);
	select @morada_cl = morada from ServEntr.Cliente where nome = @cliente;
	declare @nr_reg_cliente as int;
	select @nr_reg_cliente = nr_reg from ServEntr.Cliente where nome = @cliente;
	
	declare @nr_reg_motorista as int;
	set @nr_reg_motorista = (select top 1 nr_reg from ServEntr.Motorista);

	declare @id_aval as int;
	select @id_aval = floor(rand()*999999);

	insert into ServEntr.Avaliacao(id, obs, estrelas_compra, estrelas_motorista, tempo_espera) values (@id_aval, null, null, null, null);

	insert into ServEntr.Encomenda(id, morada, obs, preco_total, ref_pagamento, nr_reg_cliente, nr_reg_motorista, id_avaliacao, status) values (@id, @morada_cl, @obs, @preco_total, @ref, @nr_reg_cliente, @nr_reg_motorista, @id_aval, 'ativa');
	

	-- insert into ServEntr.Pagamento(preco, morada_fat, metodo, referencia) values(@preco_total, @morada_cl, @metodo, @ref);
end
GO

-- EDITA ENCOMENDA --
create proc ServEntr.editEncomenda (@id varchar(5), @status varchar(15))
as
begin
	update ServEntr.Encomenda set ServEntr.Encomenda.status = @status
	where ServEntr.Encomenda.id = @id;
end
GO

-- PREÇO TOTAL ENCOMENDA --
create proc ServEntr.newEncomendaPrice (@produtos varchar(255), @preco_total int OUTPUT)
as
begin
	declare @temp table(prod varchar(255));
	insert into @temp (prod) select * from STRING_SPLIT(@produtos, '-');
	select * from @temp;

	declare @preco as decimal(6,2);
	select @preco from ServEntr.Produto where nome=@prod;

	set @preco_total = sum(@preco);
end
GO


-- EDITA UMA AVALIACAO --
create proc ServEntr.updateAvaliacao (@id int, @obs varchar(255), @estrelas_compra int, @estrelas_motorista int, @tempo_espera int)
as
begin
	
	update ServEntr.Avaliacao set obs = @obs, estrelas_compra = @estrelas_compra, estrelas_motorista = @estrelas_motorista, tempo_espera = @tempo_espera
	where id = @id;
	
end