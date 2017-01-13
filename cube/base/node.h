#ifndef NODE_H
#define NODE_H

#include <QMatrix4x4>
#include <QVector3D>
#include <vector>
#include <string>

#define NODE_TYPE_NODE 0
#define NODE_TYPE_ENTITY 1
#define NODE_TYPE_CAMERA 2

class Scene;
class Node
{
public:
    Node();
    void addChild(Node * child);
    Node *parent() const;
    void setParent(Node *parent);
    std::vector<Node *> children() const;
    void setChildren(const std::vector<Node *> &children);
    void visit(Scene *scene);
    int nodeType() const;
    void setNodeType(int nodeType);

    void translate(float x, float y, float z);
    void rotate(float x,float y, float z);
    void scale(float x, float y ,float z);

    QMatrix4x4 getModelTrans();
    QMatrix4x4 getParentModelTrans();
    QMatrix4x4 getRotaionMatrix();
    QMatrix4x4 getScallingMatrix();
    QMatrix4x4 getTranslationMatrix();
    const char *name() ;
    void setName(const char *name);

    void setYaw(float yaw);
    float getYaw();

    void setPitch(float pitch);
    float getPitch();

    void setRoll(float roll);
    float getRoll();

    void yawBy(float value);
    void pitchBy(float value);
    void rollBy(float value);
    void moveBy(float the_x, float the_y, float the_z);
    void move(float the_x, float the_y, float the_z);

    QVector3D pos() const;
    void setPos(const QVector3D &pos);

    QVector3D rotation() const;
    void setRotation(const QVector3D &rotation);

    QVector3D scalling() const;
    void setScalling(const QVector3D &scalling);

protected:
    QVector3D m_pos;
    QVector3D m_rotation;
    QVector3D m_scalling;
    int m_nodeType;
    Node * m_parent;
    std::vector<Node *>m_children;
    std::string m_name;
};

#endif // NODE_H
