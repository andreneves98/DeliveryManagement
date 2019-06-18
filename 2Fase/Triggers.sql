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
		raiserror('Cliente já existe na Base de dados!', 16, 1);
	end
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