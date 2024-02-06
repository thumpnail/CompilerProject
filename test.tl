global record Vector 
	x: integer
	y: number
	z: number
end

local value: Vector = {
	x = 1,
	y = 2,
	z = 3,
}

function Vector.Add(self: Vector, value: Vector)
	self.x = self.x + value.x
	self.y = self.y + value.y
	self.z = self.z + value.z
end

print("x","y","z")
print(value.x, value.y, value.z)
value:Add({x=2,y=4,z=8})
print(value.x, value.y, value.z)
