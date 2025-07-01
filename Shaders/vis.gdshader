shader_type canvas_item;

uniform sampler2D noise_texture;
uniform float speed = 1.0;

void fragment() {
    vec2 uv = UV;
    vec3 noise_offset = vec3(0.0, 0.0, TIME * speed);
	
	
    COLOR = noise;
}