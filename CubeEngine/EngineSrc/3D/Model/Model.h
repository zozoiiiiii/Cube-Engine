#ifndef TZW_MODEL_H
#define TZW_MODEL_H

#include "../../Interface/Drawable3D.h"
#include "../../Mesh/Mesh.h"

namespace tzw {
class Model : public Drawable3D
{
public:
    friend class ModelLoader;
    Model();
    void initWithFile(std::string modelFilePath);
    static Model * create(std::string modelFilePath);
    virtual void draw();
private:
    Technique * m_technique;
    std::vector<Mesh *> m_meshList;
};

} // namespace tzw

#endif // TZW_MODEL_H