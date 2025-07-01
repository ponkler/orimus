shader_type canvas_item;

uniform sampler2D source_texture : filter_nearest, repeat_disable;
uniform sampler2D ascii_font : filter_nearest, repeat_disable;

uniform float char_width = 6.0;
uniform float char_height = 8.0;
uniform float char_count = 16.0;
uniform vec2 virtual_resolution = vec2(1152.0, 648.0);

void fragment() {
    vec2 pixel_pos = UV * virtual_resolution;
    vec2 cell_pos = floor(pixel_pos / vec2(char_width, char_height)) * vec2(char_width, char_height);
    vec2 center_uv = (cell_pos + vec2(char_width, char_height) * 0.5) / virtual_resolution;

    vec3 src_col = texture(source_texture, center_uv).rgb;
    float gray = dot(src_col, vec3(0.299, 0.587, 0.114));
    float char_index = floor(gray * (char_count - 1.0) + 0.5);
    float u_base = char_index / char_count;

    vec2 offset = mod(pixel_pos, vec2(char_width, char_height));

    vec2 glyph_uv = vec2(
        u_base + offset.x / (char_count * char_width),
        offset.y / char_height
    );

    vec4 glyph_col = texture(ascii_font, glyph_uv);
    COLOR = vec4(vec3(1.0, 0.192, 0.349), glyph_col.a);
}
