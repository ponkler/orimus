shader_type canvas_item;

uniform float scroll_speed = 1.0;

void fragment() {
	vec4 in_tex = texture(TEXTURE, vec2(UV.x - TIME * scroll_speed, UV.y));
	COLOR = in_tex;
}
