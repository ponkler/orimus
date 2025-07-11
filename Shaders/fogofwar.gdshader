shader_type canvas_item;

uniform sampler2D visTexture;
uniform float wobble_strength = 0.003;
uniform float wobble_frequency = 40.0;
uniform float wobble_speed = 3.0;

void fragment() {
    vec2 uv = SCREEN_UV;

    float wave = sin(uv.y * wobble_frequency + TIME * wobble_speed)
               + cos(uv.x * wobble_frequency - TIME * wobble_speed);

    vec2 offset = normalize(vec2(1.0, 1.0)) * wave * wobble_strength;

    float vis = texture(visTexture, uv + offset).r;

    COLOR.rgb = vec3(0.076, 0.058, 0.08);
    COLOR.a = 1.0 - vis;
}