shader_type canvas_item;

uniform sampler2D visTexture;

void fragment() {
	vec4 uv = texture(visTexture, SCREEN_UV);

	COLOR.rgb = vec3(0.149, 0.107, 0.16);
	COLOR.a = 1.0 - uv.r;
}