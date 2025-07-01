shader_type canvas_item;

uniform vec2 circle_positions[256];
uniform float circle_radii[256];

uniform vec2 arc_positions[256];
uniform float arc_distances[256];

uniform vec4 edge_color : source_color = vec4(0.729, 0.216, 0.961, 1.0);

uniform float edge_width = 20.0;
uniform float noise_scale = 1.0;
uniform float noise_strength = 10.0;

void fragment() {
	COLOR.rgb = vec3(0.141, 0.102, 0.149);
	COLOR.a = 1.0;

	// Circle cutouts
	for (int i = 0; i < circle_positions.length(); i++) {
		vec2 invalid = vec2(0.0, 0.0);

		if (circle_positions[i] == invalid) {
			continue;
		}

		float dist = distance(VERTEX, circle_positions[i]);

		if (dist < circle_radii[i]) {
	        COLOR.a = 0.0;
	    }
	}

	// Arc cutouts
	for (int i = 0; i < arc_positions.length(); i++) {
		vec2 invalid = vec2(0.0, 0.0);
		if (arc_positions[i] == invalid) {
			continue;
		}

		vec2 delta = VERTEX - arc_positions[i];
		float dist = length(delta);

		float max_dist = arc_distances[i];
		float angle_to_pixel = atan(delta.y, delta.x);

		float arc_center_angle = 0.0;
		float arc_width = 1.57;

		float angle_diff = abs(angle_to_pixel - arc_center_angle);
		if (angle_diff > 3.141592) {
			angle_diff = 6.283185 - angle_diff;
		}

		if (angle_diff < arc_width * 0.5 && dist < max_dist) {
			COLOR.a = 0.0;
		}
	}

}