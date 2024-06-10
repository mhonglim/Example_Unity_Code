import bpy
from pathlib import Path

#reset scene
bpy.ops.object.select_all(action="SELECT")
bpy.ops.object.delete()

#import nasal splint model
Import_Location1 = "C://Users//acer//NasalSplint_V2//Result//Export1.stl"
bpy.ops.import_mesh.stl(filepath = Import_Location1)

#add subdivision surface and smooth
bpy.ops.object.modifier_add(type = "SUBSURF")
bpy.data.objects[0].modifiers["Subdivision"].levels = 2
bpy.ops.object.modifier_add(type = 'SMOOTH')
#bpy.data.object[0].modifiers["Smooth"].factor = 1
bpy.data.objects[0].modifiers["Smooth"].iterations = 200

#add shade smooth
bpy.ops.object.shade_smooth()

#export nasal splint model
Export_Location = "C://Users//acer//NasalSplint_V2//Result_Smooth//"
#bpy.ops.export_mesh.stl(filepath = Export_Location)

context = bpy.context
scene = context.scene
viewlayer = context.view_layer

obs = [o for o in scene.objects if o.type == 'MESH']
bpy.ops.object.select_all(action='DESELECT')

path = Path(Export_Location)
for ob in obs:
    viewlayer.objects.active = ob
    ob.select_set(True)
    stl_path = path / f"{ob.name}.stl"
    bpy.ops.export_mesh.stl(
            filepath=str(stl_path),
            use_selection=True)
    ob.select_set(False)