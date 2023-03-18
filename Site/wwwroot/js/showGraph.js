// once everything is loaded, we run our Three.js stuff.
var stats = initStats();

// create a scene, that will hold all our elements such as objects, cameras and lights.
var scene = new THREE.Scene();

// create a camera, which defines where we're looking at.
var camera = new THREE.PerspectiveCamera(45, window.innerWidth / window.innerHeight, 0.1, 1000);

// create a render and set the size
var webGLRenderer = new THREE.WebGLRenderer({ antialias: true });
webGLRenderer.setClearColor(new THREE.Color(0xEEEEEE, 1.0));
webGLRenderer.setSize(window.innerWidth / 2, window.innerHeight / 2);
webGLRenderer.shadowMapEnabled = true;

var axisHelper = new THREE.AxisHelper(5);
scene.add(axisHelper);

// position and point the camera to the center of the scene
camera.position.set(0, 20, 35);
camera.lookAt(new THREE.Vector3(0, 0, 0));

// add the output of the renderer to the html element
$("#WebGL-output").append(webGLRenderer.domElement);

// call the render function
var step = 0;

// the points group
var spGroup;
// the mesh
var hullMesh;

var rotSpeed = .01

render();

function generatePoints() {
    var points = convert(pointsData);

    spGroup = new THREE.Object3D();
    var material = new THREE.MeshBasicMaterial({ color: 0xff0000, transparent: false });
    points.forEach(function (point) {

        var spGeom = new THREE.SphereGeometry(0.2);
        var spMesh = new THREE.Mesh(spGeom, material);
        spMesh.position = point;
        spGroup.add(spMesh);
    });
    // add the points as a group to the scene
    scene.add(spGroup);

    // use the same points to create a convexgeometry
    var hullGeometry = new THREE.ConvexGeometry(points);
    hullMesh = createMesh(hullGeometry);
    scene.add(hullMesh);
}

function convert(points) {
    var splita = points.split(';', 10000);
    var newpoint = [];
    for (var i = 0; i < splita.length; i++) {
        var tmp = splita[i].split(':', 10000);
        newpoint.push(new THREE.Vector3(tmp[0], tmp[1], tmp[2]));
    }

    return newpoint;
}

function createMesh(geom) {

    // assign two materials
    var meshMaterial = new THREE.MeshBasicMaterial({ color: 0x00ff00, transparent: true, opacity: 0.2 });
    meshMaterial.side = THREE.DoubleSide;
    var wireFrameMat = new THREE.MeshBasicMaterial();
    wireFrameMat.wireframe = true;

    // create a multimaterial
    var mesh = THREE.SceneUtils.createMultiMaterialObject(geom, [meshMaterial, wireFrameMat]);

    return mesh;
}

function render() {
    stats.update();

    // Camera rotation
    var x = camera.position.x,
        y = camera.position.y,
        z = camera.position.z;

    camera.position.x = x * Math.cos(rotSpeed) + z * Math.sin(rotSpeed);
    camera.position.z = z * Math.cos(rotSpeed) - x * Math.sin(rotSpeed);
    camera.lookAt(scene.position);

    // render using requestAnimationFrame
    requestAnimationFrame(render);
    webGLRenderer.render(scene, camera);
}

function initStats() {

    var stats = new Stats();
    stats.setMode(0); // 0: fps, 1: ms

    // Align top-left
    stats.domElement.style.position = 'absolute';
    stats.domElement.style.left = '0px';
    stats.domElement.style.top = '0px';

    $("#Stats-output").append(stats.domElement);

    return stats;
}
