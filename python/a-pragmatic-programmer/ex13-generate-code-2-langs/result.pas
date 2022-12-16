{ Add a product }
{ to the 'on-order' list }
AddProductMsg = packed record
	id: int;
	name: array[0-29] of char;
	order_code: int;
end;
