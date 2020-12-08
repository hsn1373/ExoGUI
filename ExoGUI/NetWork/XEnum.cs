using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwinCAT.TypeSystem;

namespace ExoGUI.NetWork
{
    public enum X
    {
        [Type(TypeAttribute.Types.UDInt, TypeAttribute.RW.FullControll, sourceFunction: "GVL", notify: false)]
        Gui_manager,
        [Type(TypeAttribute.Types.Real, TypeAttribute.RW.FullControll, sourceFunction: "GVL", notify: false)]
        Position_mode_right_hip,
        [Type(TypeAttribute.Types.Real, TypeAttribute.RW.FullControll, sourceFunction: "GVL", notify: false)]
        Position_mode_right_knee,
        [Type(TypeAttribute.Types.Real, TypeAttribute.RW.FullControll, sourceFunction: "GVL", notify: false)]
        Position_mode_left_hip,
        [Type(TypeAttribute.Types.Real, TypeAttribute.RW.FullControll, sourceFunction: "GVL", notify: false)]
        Position_mode_left_knee,
        [Type(TypeAttribute.Types.UDInt, TypeAttribute.RW.FullControll, sourceFunction: "GVL", notify: false)]
        StartTrajLen,
        [Type(TypeAttribute.Types.UDInt, TypeAttribute.RW.FullControll, sourceFunction: "GVL", notify: false)]
        RightTrajLen,
        [Type(TypeAttribute.Types.UDInt, TypeAttribute.RW.FullControll, sourceFunction: "GVL", notify: true)]
        BufferStatus,
        [Type(TypeAttribute.Types.Bool, TypeAttribute.RW.FullControll, sourceFunction: "GVL", notify: false)]
        ReachEndOfTraj,
        [Type(TypeAttribute.Types.UDInt, TypeAttribute.RW.FullControll, sourceFunction: "GVL", notify: true)]
        WhichEndOfTraj,
        [Type(TypeAttribute.Types.Real, TypeAttribute.RW.FullControll, sourceFunction: "GVL", notify: false)]
        BufferPos1,
        [Type(TypeAttribute.Types.Real, TypeAttribute.RW.FullControll, sourceFunction: "GVL", notify: false)]
        BufferPos2,
        [Type(TypeAttribute.Types.Bool, TypeAttribute.RW.FullControll, sourceFunction: "GVL", notify: true)]
        ReadActualPos,
        [Type(TypeAttribute.Types.DInt, TypeAttribute.RW.FullControll, sourceFunction: "GVL", notify: false)]
        TargetPosition,
        [Type(TypeAttribute.Types.DInt, TypeAttribute.RW.FullControll, sourceFunction: "GVL", notify: false)]
        TargetPosition2,
        [Type(TypeAttribute.Types.DInt, TypeAttribute.RW.FullControll, sourceFunction: "GVL", notify: false)]
        TargetPosition3,
        [Type(TypeAttribute.Types.DInt, TypeAttribute.RW.FullControll, sourceFunction: "GVL", notify: false)]
        TargetPosition4,
        [Type(TypeAttribute.Types.DInt, TypeAttribute.RW.FullControll, sourceFunction: "GVL", notify: false)]
        TargetData,
        [Type(TypeAttribute.Types.DInt, TypeAttribute.RW.FullControll, sourceFunction: "GVL", notify: false)]
        TargetData2,
        [Type(TypeAttribute.Types.DInt, TypeAttribute.RW.FullControll, sourceFunction: "GVL", notify: false)]
        TargetData3,
        [Type(TypeAttribute.Types.DInt, TypeAttribute.RW.FullControll, sourceFunction: "GVL", notify: false)]
        TargetData4,
        [Type(TypeAttribute.Types.DInt, TypeAttribute.RW.FullControll, sourceFunction: "GVL", notify: false)]
        PositionDesiredVal1,
        [Type(TypeAttribute.Types.DInt, TypeAttribute.RW.FullControll, sourceFunction: "GVL", notify: false)]
        PositionDesiredVal2,
        [Type(TypeAttribute.Types.DInt, TypeAttribute.RW.FullControll, sourceFunction: "GVL", notify: false)]
        PositionDesiredVal3,
        [Type(TypeAttribute.Types.DInt, TypeAttribute.RW.FullControll, sourceFunction: "GVL", notify: false)]
        PositionDesiredVal4,
        [Type(TypeAttribute.Types.DInt, TypeAttribute.RW.FullControll, sourceFunction: "GVL", notify: false)]
        loadCellDesiredVal1,
        [Type(TypeAttribute.Types.DInt, TypeAttribute.RW.FullControll, sourceFunction: "GVL", notify: false)]
        loadCellDesiredVal2,
        [Type(TypeAttribute.Types.DInt, TypeAttribute.RW.FullControll, sourceFunction: "GVL", notify: false)]
        loadCellDesiredVal3,
        [Type(TypeAttribute.Types.DInt, TypeAttribute.RW.FullControll, sourceFunction: "GVL", notify: false)]
        loadCellDesiredVal4,
        [Type(TypeAttribute.Types.DInt, TypeAttribute.RW.FullControll, sourceFunction: "GVL", notify: false)]
        LoadcellLeftHip,
        [Type(TypeAttribute.Types.DInt, TypeAttribute.RW.FullControll, sourceFunction: "GVL", notify: false)]
        LoadcellLeftKnee,
        [Type(TypeAttribute.Types.DInt, TypeAttribute.RW.FullControll, sourceFunction: "GVL", notify: false)]
        LoadcellRightHip,
        [Type(TypeAttribute.Types.DInt, TypeAttribute.RW.FullControll, sourceFunction: "GVL", notify: false)]
        LoadcellRightknee,
        [Type(TypeAttribute.Types.DInt, TypeAttribute.RW.FullControll, sourceFunction: "GVL", notify: false)]
        LeftFootFrontSensor,
        [Type(TypeAttribute.Types.DInt, TypeAttribute.RW.FullControll, sourceFunction: "GVL", notify: false)]
        LeftFootRearSensor,
        [Type(TypeAttribute.Types.DInt, TypeAttribute.RW.FullControll, sourceFunction: "GVL", notify: false)]
        RightFootFrontSensor,
        [Type(TypeAttribute.Types.DInt, TypeAttribute.RW.FullControll, sourceFunction: "GVL", notify: false)]
        RightFootRearSensor,
        [Type(TypeAttribute.Types.DInt, TypeAttribute.RW.FullControll, sourceFunction: "GVL", notify: false)]
        TimeFromPC,
        [Type(TypeAttribute.Types.DInt, TypeAttribute.RW.FullControll, sourceFunction: "GVL", notify: false)]
        PreTimeFromPC,
        [Type(TypeAttribute.Types.Int, TypeAttribute.RW.FullControll, sourceFunction: "GVL", notify: true)]
        BufferActualStatus,
        [Type(TypeAttribute.Types.Bool, TypeAttribute.RW.FullControll, sourceFunction: "GVL", notify: false)]
        StartRecordFlag,
        [Type(TypeAttribute.Types.DInt, TypeAttribute.RW.FullControll, length: 500, sourceFunction: "GVL", notify: false)]
        PositionActualValBuffer1,
        [Type(TypeAttribute.Types.DInt, TypeAttribute.RW.FullControll, length: 500, sourceFunction: "GVL", notify: false)]
        PositionActualValBuffer2,
        [Type(TypeAttribute.Types.DInt, TypeAttribute.RW.FullControll, length: 500, sourceFunction: "GVL", notify: false)]
        PositionActualValBuffer3,
        [Type(TypeAttribute.Types.DInt, TypeAttribute.RW.FullControll, length: 500, sourceFunction: "GVL", notify: false)]
        CurrentValBuffer1,
        [Type(TypeAttribute.Types.DInt, TypeAttribute.RW.FullControll, length: 500, sourceFunction: "GVL", notify: false)]
        CurrentValBuffer2,
        [Type(TypeAttribute.Types.DInt, TypeAttribute.RW.FullControll, length: 500, sourceFunction: "GVL", notify: false)]
        CurrentValBuffer3,
        [Type(TypeAttribute.Types.DInt, TypeAttribute.RW.FullControll, length: 500, sourceFunction: "GVL", notify: false)]
        LoadCellValBuffer1,
        [Type(TypeAttribute.Types.DInt, TypeAttribute.RW.FullControll, length: 500, sourceFunction: "GVL", notify: false)]
        LoadCellValBuffer2,
        [Type(TypeAttribute.Types.DInt, TypeAttribute.RW.FullControll, length: 500, sourceFunction: "GVL", notify: false)]
        LoadCellValBuffer3,
        [Type(TypeAttribute.Types.DInt, TypeAttribute.RW.FullControll, length: 500, sourceFunction: "GVL", notify: false)]
        FootSensorBuffer1,
        [Type(TypeAttribute.Types.DInt, TypeAttribute.RW.FullControll, length: 500, sourceFunction: "GVL", notify: false)]
        FootSensorBuffer2,
        [Type(TypeAttribute.Types.DInt, TypeAttribute.RW.FullControll, length: 500, sourceFunction: "GVL", notify: false)]
        PositionDesiredValBuffer1,
        [Type(TypeAttribute.Types.DInt, TypeAttribute.RW.FullControll, length: 500, sourceFunction: "GVL", notify: false)]
        PositionDesiredValBuffer2,
        [Type(TypeAttribute.Types.DInt, TypeAttribute.RW.FullControll, length: 500, sourceFunction: "GVL", notify: false)]
        LoadCellDesiredValBuffer1,
        [Type(TypeAttribute.Types.DInt, TypeAttribute.RW.FullControll, length: 500, sourceFunction: "GVL", notify: false)]
        LoadCellDesiredValBuffer2,
        [Type(TypeAttribute.Types.DInt, TypeAttribute.RW.FullControll, length: 500, sourceFunction: "GVL", notify: false)]
        EMGRightValBuffer1,
        [Type(TypeAttribute.Types.DInt, TypeAttribute.RW.FullControll, length: 500, sourceFunction: "GVL", notify: false)]
        EMGRightValBuffer2,
        [Type(TypeAttribute.Types.DInt, TypeAttribute.RW.FullControll, length: 500, sourceFunction: "GVL", notify: false)]
        EMGLeftValBuffer1,
        [Type(TypeAttribute.Types.DInt, TypeAttribute.RW.FullControll, length: 500, sourceFunction: "GVL", notify: false)]
        EMGLeftValBuffer2
    }
}
