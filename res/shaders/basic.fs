#ifdef GL_ES
// Set default precision to medium
precision mediump int;
precision mediump float;
#endif           
                                     
const int MAX_SPOT_LIGHTS = 2;    
struct SpotLight
{
	vec3 direction;
	float range;
	vec3 pos;
	vec3 color;
	float intensity;
	float ang;
	float outterAng;
};

uniform int spotLightAmount;
uniform SpotLight spotLight[MAX_SPOT_LIGHTS];

uniform sampler2D texture;
uniform sampler2D ShadowMap;


varying vec3 world_position;
varying vec4 LightSpace_postion;
varying vec2 v_texcoord;
varying vec3 v_normal_line;



float CalcShadowFactor(sampler2D the_shadow_map, vec4 LightSpacePos)                                                  
{                       	
	float bias = 0.005;
    vec3 ProjCoords = LightSpacePos.xyz / LightSpacePos.w;                                  
    vec2 UVCoords;                                                                          
    UVCoords.x = 0.5*ProjCoords.x +0.5;                                                  
    UVCoords.y = 0.5*ProjCoords.y+0.5;                                                  
    float z =  0.5*ProjCoords.z+0.5;

	float xOffset = 1.0/2000;
    float yOffset = 1.0/2000;

    float Factor = 0.0;

    for (int y = -1 ; y <= 1 ; y++) {
        for (int x = -1 ; x <= 1 ; x++) {
            vec2 Offsets = vec2(x * xOffset, y * yOffset);
            vec2 UVC = vec2(UVCoords + Offsets);
            float  result = texture2D(the_shadow_map, UVC).x;
			if(result < z -0.00001)
			{
			Factor +=0;
			}
			else{
			Factor +=1;	
			}
        }
    }                                                                    
	return Factor/9;
}


vec4 calculateDiffuse(vec3 normal_line , vec3 light_direction , vec3 color , float intensity)
{
	float diffuse_factor = dot(normalize(normal_line), normalize(-light_direction));
	
	vec4 diffuse_color;
    if (diffuse_factor > 0) {
        diffuse_color = vec4(color,1.0) * diffuse_factor * intensity;
    }
    else {
        diffuse_color = vec4(0, 0, 0, 1.0);
    }
	return diffuse_color;
}



vec4 calculateSpotLight(vec3 normal_line,SpotLight light)
{
	float shadow_factor =CalcShadowFactor(ShadowMap,LightSpace_postion);
	vec3 pixelToSourceDirection = world_position - light.pos;
	float distance = length(pixelToSourceDirection);
	pixelToSourceDirection = normalize(pixelToSourceDirection);
	float current_ang = dot(normalize(pixelToSourceDirection), normalize(light.direction));
	if(current_ang > light.ang)
	{
		vec4 pointColor = calculateDiffuse(normal_line,light.direction, light.color , light.intensity);
		float attenuation = clamp (1 - (distance/ light.range ),0,1);
		pointColor *=  attenuation;
		return shadow_factor*pointColor;
	}else if(current_ang > light.outterAng){
		vec4 pointColor = calculateDiffuse(normal_line,light.direction, light.color , light.intensity);
		float attenuation = clamp (1 - (distance/ light.range ),0,1);
		attenuation *= smoothstep(  light.outterAng , light.ang , current_ang);
		pointColor *=  attenuation;
		return shadow_factor*pointColor;
	}
	else {
			return vec4(0.0,0,0,1.0);
	}

}




void main()
{
	vec3 normal;
		normal = v_normal_line;

	vec4 totalLight = vec4(0,0,0,1.0);	
	for(int j = 0;j<spotLightAmount;j++)
	{
	totalLight += calculateSpotLight(normal,spotLight[j]);
	}
	
	gl_FragColor = texture2D(texture,v_texcoord)*totalLight;
	return;
}

