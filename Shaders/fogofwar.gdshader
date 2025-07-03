shader_type canvas_item;

uniform sampler2D visTexture;

void fragment() {
	vec4 uv = texture(visTexture, UV);
	COLOR.a = 1.0 - uv.r;
}