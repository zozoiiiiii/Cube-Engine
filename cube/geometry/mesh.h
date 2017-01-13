#ifndef MESH_H
#define MESH_H
#include "base/vertexdata.h"
#include <vector>
#include <QGLFunctions>
#include <QGLShaderProgram>
#include "renderer/meshdrawcomand.h"
#include "shader/shader_program.h"
#include "material/material.h"
#include  "base/bonedata.h"
class TMesh: protected QGLFunctions
{
public:
    TMesh();
    void pushVertex(VertexData vertex);
    void pushVertex(VertexData * vertex,int size);
    void pushIndex(GLushort index);
    void pushIndex(GLushort * array, int size);
    void initWithoutNormal();
    void setBones(std::vector<BoneData> bones);
    void init();
    void checkIndexValid(int index);
    void draw();
    int getVerticesNumber();
    VertexData * at(int index);
    GLuint getVerticesVbo();
    GLuint getIndicesVbo();
    MeshDrawComand *getCommand();
    Material *getMaterial() const;
    void setMaterial(Material *value);
private:

    Material * material;
    MeshDrawComand draw_command;
    GLuint vbo[3];
    std::vector<VertexData> vertices;
    int texture_id;
    std::vector<GLushort > indices;
    std::vector<BoneData> m_bones;
};

#endif // MESH_H
